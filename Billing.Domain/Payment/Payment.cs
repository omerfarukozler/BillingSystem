using Billing.Domain.Shared;
using Billing.Domain.Shared.ValueObjects;

namespace Billing.Domain.Payment;

public class Payment : BaseEntity
{
    public Guid InvoiceId { get; private set; }
    public Money Amount { get; private set; } = Money.Zero();
    public PaymentStatus Status { get; private set; }

    private Payment() { }

    public Payment(Guid invoiceId, Money amount)
    {
        InvoiceId = invoiceId;
        Amount = amount;
        Status = PaymentStatus.Processing;
    }

    public void MarkAsCompleted()
    {
        if (Status == PaymentStatus.Completed)
            throw new InvalidOperationException("Payment already completed.");

        Status = PaymentStatus.Completed;
    }
}
