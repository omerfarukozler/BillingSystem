namespace Billing.Domain.Customer;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task DeleteAsync(Customer customer);
    Task<Customer?> GetByIdAsync(Guid customerId);
}
