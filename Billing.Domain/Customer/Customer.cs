using Billing.Domain.Shared;

namespace Billing.Domain.Customer;

public class Customer : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Customer(string name, string email)
    {
        Name = name;
        Email = email;
    }
}
