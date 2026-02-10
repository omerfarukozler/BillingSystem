using Billing.App;
using Billing.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service)
    {
        _service = service;
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

    [Authorize]
    [HttpGet("customer-list")]
    [ProducesResponseType(typeof(ListCustomerResponse[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListCustomers()
    {
        var customers = await _service.ListCustomersAsync();
        return Ok(customers);
    }
}

public record SignupRequest(string Email, string Password, string Name);
public record LoginRequest(string Email, string Password);
