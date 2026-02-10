using Billing.App.Ports;
using Billing.Domain.Customer;

namespace Billing.App;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepo;
    private readonly ICurrentAccount _currentAccount;

    public CustomerService(ICustomerRepository customerRepo, ICurrentAccount currentAccount)
    {
        _customerRepo = customerRepo;
        _currentAccount = currentAccount;
    }

    public async Task AddAsync(string name, string email)
    {
        var accountId = _currentAccount.AccountId;
        var customer = new Customer(accountId, name, email);
        await _customerRepo.AddAsync(customer);
    }

    public async Task DeleteAsync(Guid customerId)
    {
        var customer = await _customerRepo.GetByIdAsync(customerId);
        if (customer is null) return;

        if (customer.AccountId != _currentAccount.AccountId) return;

        await _customerRepo.DeleteAsync(customer);
    }
}