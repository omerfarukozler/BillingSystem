namespace Billing.Domain.Account;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task<Account?> GetByEmailAsync(string email);
}
