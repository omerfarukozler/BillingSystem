using Billing.Domain.Account;
using Microsoft.EntityFrameworkCore;
using Billing.Domain.Customer;

namespace Billing.Infra.Data.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BillingDbContext dbContext;

    public AccountRepository(BillingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Account?> GetByEmailAsync(string email)
    {
        return await dbContext.Accounts
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task AddAsync(Account account)
    {
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync();
    }
    public async Task<List<Customer>> ListCustomers(Guid accountId)
    {
        var customer = await dbContext.Customers.Where(x => x.AccountId == accountId).ToListAsync();
        return customer;
    }
}
