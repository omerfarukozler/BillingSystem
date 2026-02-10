using Billing.Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace Billing.Infra.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly BillingDbContext dbContext;

    public CustomerRepository(BillingDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task AddAsync(Customer customer)
    {
        dbContext.Customers.Add(customer);
        await dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Customer customer)
    {
        dbContext.Customers.Remove(customer);
        await dbContext.SaveChangesAsync();
    }
    public async Task<Customer?> GetByIdAsync(Guid customerId)
    {
        return await dbContext.Customers.SingleOrDefaultAsync(x => x.Id == customerId);
    }

}
