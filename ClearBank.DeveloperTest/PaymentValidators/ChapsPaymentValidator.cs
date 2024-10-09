using ClearBank.Application.DTOs;
using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;

namespace ClearBank.Application.PaymentValidators
{
    public class ChapsPaymentValidator : BasePaymentValidator
    {
        public override AllowedPaymentSchemes AllowedPaymentSchemes => AllowedPaymentSchemes.Chaps;
        public override PaymentScheme PaymentScheme => PaymentScheme.Chaps;

        public override bool ValidatePayment(Account account, MakePaymentRequest makePaymentRequest = null)
        {
            var result = base.ValidatePayment(account);
            if (account.Status != AccountStatus.Live)
            {
                result = false;
            }
            return result;
        }
    }
}
