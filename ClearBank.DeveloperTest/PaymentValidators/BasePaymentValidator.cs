using ClearBank.Application.DTOs;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;

namespace ClearBank.Application.PaymentValidators
{
    public abstract class BasePaymentValidator : IPaymentValidator
    {
        public virtual AllowedPaymentSchemes AllowedPaymentSchemes { get; }
        public virtual PaymentScheme PaymentScheme { get; }

        public virtual bool ValidatePayment(Account account, MakePaymentRequest makePaymentRequest = null)
        {
            var result = true;
            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes))
            {
                result = false;
            }
            return result;
        }
    }
}
