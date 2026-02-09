using Billing.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _service;

    public CustomerController(CustomerService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddCustomer(AddCustomerRequest request)
    {
        await _service.AddAsync(request.AccountId, request.Name, request.Email);
        return Ok(new { message = "Customer created" });
    }
}
