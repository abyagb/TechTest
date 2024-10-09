using ClearBank.Application.DTOs;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;

namespace ClearBank.Application.PaymentValidators
{
    public class FasterPaymentsValidator : BasePaymentValidator
    {
        public override AllowedPaymentSchemes AllowedPaymentSchemes => AllowedPaymentSchemes.FasterPayments;
        public override PaymentScheme PaymentScheme => PaymentScheme.FasterPayments;

        public override bool ValidatePayment(Account account, MakePaymentRequest makePaymentRequest)
        {
            var result = base.ValidatePayment(account, makePaymentRequest);
            if (account.Balance < makePaymentRequest.Amount)
            {
                result = false;
            }
            return result;
        }
    }
}
