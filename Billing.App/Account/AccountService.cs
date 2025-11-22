using Billing.Domain.Account;

namespace Billing.App;

public class AccountService
{
    private readonly IAccountRepository _accountRepo;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwt;

    public AccountService(
        IAccountRepository accountRepo,
        IPasswordHasher hasher,
        IJwtTokenGenerator jwt)
    {
        _accountRepo = accountRepo;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task RegisterAsync(string email, string password, string name)
    {
        var existing = await _accountRepo.GetByEmailAsync(email);
        if (existing != null)
            throw new Exception("Email already registered");

        var hash = _hasher.Hash(password);

        var account = new Account(email, hash, name);
        await _accountRepo.AddAsync(account);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var account = await _accountRepo.GetByEmailAsync(email);

        if (account is null)
            throw new Exception("Invalid email or password");

        if (!_hasher.Verify(password, account.PasswordHash))
            throw new Exception("Invalid email or password");

        var token = _jwt.GenerateToken(account);
        return token;
    }
    public async Task<Account?> ValidateAsync(string email, string password)
{
    var account = await _accountRepo.GetByEmailAsync(email);
    if (account == null)
        return null;

    var valid = _hasher.Verify(password, account.PasswordHash);
    if (!valid)
        return null;

    return account;
}

}
