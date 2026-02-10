using Billing.Domain.Customer;

namespace Billing.App;

public class ListCustomerResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public static ListCustomerResponse FromEntity(Customer customer)
    {
        var response = new ListCustomerResponse();

        response.Id = customer.Id;
        response.Name = customer.Name;
        response.Email = customer.Email;
        response.CreatedAt = customer.CreatedAt;

        return response;
    }
}