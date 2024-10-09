using ClearBank.Application.DTOs;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;

namespace ClearBank.Application.PaymentValidators
{
    public interface IPaymentValidator
    {
        AllowedPaymentSchemes AllowedPaymentSchemes { get; }
        PaymentScheme PaymentScheme { get; }
        bool ValidatePayment(Account account, MakePaymentRequest makePaymentRequest = null);
    }
}
