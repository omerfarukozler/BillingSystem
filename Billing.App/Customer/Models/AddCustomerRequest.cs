namespace Billing.App;

public class AddCustomerRequest
{
    public Guid AccountId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
}
