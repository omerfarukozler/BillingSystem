namespace Billing.Domain.Account;
using Billing.Domain.Customer;

public interface IAccountRepository
{
    Task<Account?> GetByEmailAsync(string email);
    Task AddAsync(Account account);
    Task<List<Customer>> ListCustomers(Guid accountId);
}
