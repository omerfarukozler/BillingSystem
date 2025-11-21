using Billing.Domain.Shared;
using Billing.Domain.Shared.ValueObjects;

namespace Billing.Domain.Invoice;

public class Invoice : BaseEntity
{
    public string Number { get; private set; } = string.Empty;
    public Guid CustomerId { get; private set; }
    public Money Amount { get; private set; } = Money.Zero();
    public InvoiceStatus Status { get; private set; }

    private Invoice() { }

    public Invoice(string number, Guid customerId, Money amount)
    {
        Number = number;
        CustomerId = customerId;
        Amount = amount;
        Status = InvoiceStatus.Pending;
    }

    public void MarkAsPaid()
    {
        if (Status == InvoiceStatus.Paid)
            throw new InvalidOperationException("Invoice already paid.");

        Status = InvoiceStatus.Paid;
    }
}
