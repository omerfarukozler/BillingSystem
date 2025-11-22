using Billing.Domain.Account;
public class AccountService
{
    private readonly IAccountRepository _accountRepo;
    private readonly IPasswordHasher _hasher;

    public AccountService(IAccountRepository repo, IPasswordHasher hasher)
    {
        _accountRepo = repo;
        _hasher = hasher;
    }

    public async Task CreateAccountAsync(string email, string password, string name)
    {
        var existing = await _accountRepo.GetByEmailAsync(email);
        if (existing != null)
            throw new Exception("Email already registered");

        var hash = _hasher.Hash(password);

        var account = new Account(email, hash, name);
        await _accountRepo.AddAsync(account);
    }
}
