using Billing.Domain.Customer;

namespace Billing.App;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepo;

    public CustomerService(ICustomerRepository accountRepo)
    {
        _customerRepo = accountRepo;
    }

    public async Task AddAsync(Guid accountId, string name, string email)
    {
        var customer = new Customer(accountId, name, email);
        await _customerRepo.AddAsync(customer);
    }

}
