namespace Billing.Domain.Account;

public interface IJwtTokenGenerator
{
    string GenerateToken(Account account);
}
