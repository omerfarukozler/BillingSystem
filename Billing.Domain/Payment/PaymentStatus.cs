namespace Billing.Domain.Payment;

public enum PaymentStatus
{
    Processing = 0,
    Completed = 1,    
    Failed = 2,       
    Refunded = 3
}
