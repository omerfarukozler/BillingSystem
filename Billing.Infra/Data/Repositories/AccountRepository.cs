using Billing.Domain.Account;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BillingDbContext _db;

    public AccountRepository(BillingDbContext db)
    {
        _db = db;
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await _db.Accounts
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(Account account)
    {
        _db.Accounts.Add(account);
        await _db.SaveChangesAsync();
    }
}
