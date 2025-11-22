using Billing.Domain.Account;
using Billing.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra;

public class AccountRepository : IAccountRepository
{
    private readonly BillingDbContext _context;

    public AccountRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(a => a.Email == email);
    }
}
