using Billing.Domain.Shared;

namespace Billing.Domain.Customer;

public class Customer : BaseEntity
{
    public Guid AccountId { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    private Customer() { }

    public Customer(Guid accountId, string name, string email)
    {
        AccountId = accountId;
        Name = name;
        Email = email;
    }
}
