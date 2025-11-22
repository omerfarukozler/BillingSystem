using Microsoft.EntityFrameworkCore;
using Billing.Domain.Invoice;
using Billing.Domain.Customer;
using Billing.Domain.Payment;
using Billing.Domain.Account;

namespace Billing.Infra.Data;

public class BillingDbContext : DbContext
{
    public BillingDbContext(DbContextOptions<BillingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Account> Accounts => Set<Account>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Invoice>()
            .OwnsOne(i => i.Amount);

        modelBuilder.Entity<Payment>()
            .OwnsOne(p => p.Amount);
    }
}
