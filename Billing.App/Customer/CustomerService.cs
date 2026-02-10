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
        if (customer is null)
            throw new KeyNotFoundException("Customer not found.");

        if (customer.AccountId != _currentAccount.AccountId)
            throw new UnauthorizedAccessException("You are not authorized to delete this customer.");

        await _customerRepo.DeleteAsync(customer);
    }
}
