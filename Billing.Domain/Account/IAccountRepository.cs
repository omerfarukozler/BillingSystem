public interface IAccountRepository
{
    Task<Account?> GetByEmailAsync(string email);
    Task AddAsync(Account account);
}
