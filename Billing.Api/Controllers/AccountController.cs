using Billing.App;
using Billing.Domain.Account;
using Billing.Infra.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;
    private readonly Domain.Account.IJwtTokenGenerator _jwt;

    public AccountController(AccountService service, Domain.Account.IJwtTokenGenerator jwt)
    {
        _service = service;
        _jwt = jwt;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SignupRequest request)
    {
        await _service.RegisterAsync(request.Email, request.Password, request.Name);
        return Ok(new { message = "Account created" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var account = await _service.ValidateAsync(request.Email, request.Password);
        if (account == null)
            return Unauthorized(new { message = "Invalid credentials" });

        var token = _service.LoginAsync(account.Email, request.Password);

        return Ok(new { token });
    }
}

public record SignupRequest(string Email, string Password, string Name);
public record LoginRequest(string Email, string Password);
