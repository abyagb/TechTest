using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;

namespace ClearBank.Application.Resolvers
{
    public interface IPaymentValidatorResolver
    {
        IPaymentValidator GetPaymentValidator(PaymentScheme paymentScheme);
    }
}
