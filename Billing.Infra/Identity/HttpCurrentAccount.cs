using System.Security.Claims;
using Billing.App.Ports;
using Microsoft.AspNetCore.Http;

namespace Billing.Api.Adapters;

public class HttpCurrentAccount : ICurrentAccount
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpCurrentAccount(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid AccountId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User
                       ?? throw new UnauthorizedAccessException("No HttpContext/User");

            var val = user.FindFirstValue("accountId")
                      ?? user.FindFirstValue(ClaimTypes.NameIdentifier)
                      ?? user.FindFirstValue("sub");

            if (!Guid.TryParse(val, out var accountId))
                throw new UnauthorizedAccessException("accountId claim missing/invalid");

            return accountId;
        }
    }
}
