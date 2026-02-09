namespace Billing.Domain.Customer;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
}
