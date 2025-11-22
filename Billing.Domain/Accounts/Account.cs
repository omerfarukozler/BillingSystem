using Billing.Domain.Shared;

namespace Billing.Domain.Account;

public class Account : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;

    private Account() { }

    public Account(string email, string passwordHash, string name)
    {
        Email = email;
        PasswordHash = passwordHash;
        Name = name;
    }

    public void ChangePassword(string newHash)
    {
        PasswordHash = newHash;
    }
}
