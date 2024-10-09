using ClearBank.Application.DTOs;
using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;

namespace ClearBank.Application.PaymentValidators
{
    public class BacsPaymentValidator : BasePaymentValidator
    {
        public override AllowedPaymentSchemes AllowedPaymentSchemes => AllowedPaymentSchemes.Bacs;
        public override PaymentScheme PaymentScheme => PaymentScheme.Bacs;

        public override bool ValidatePayment(Account account, MakePaymentRequest makePaymentRequest = null)
        {
            var result = base.ValidatePayment(account);
            return result;
        }
    }
}
