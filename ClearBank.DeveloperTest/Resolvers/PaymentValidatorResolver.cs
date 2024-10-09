using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClearBank.Application.Resolvers
{
    public class PaymentValidatorResolver : IPaymentValidatorResolver
    {
        private readonly Dictionary<PaymentScheme, IPaymentValidator> _paymentValidatorDictionary;

        public PaymentValidatorResolver(IEnumerable<IPaymentValidator> paymentValidators)
        {
            _paymentValidatorDictionary = paymentValidators.ToDictionary(validator => validator.PaymentScheme);
        }

        public IPaymentValidator GetPaymentValidator(PaymentScheme paymentScheme)
        {
            if (_paymentValidatorDictionary.TryGetValue(paymentScheme, out var validator))
            {
                return validator;
            }

            throw new ArgumentException($"No validator found for payment scheme : {paymentScheme}");
        }
    }
}
