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
}
