using ClearBank.Application.DTOs;
using ClearBank.Application.Interfaces;
using ClearBank.Application.Resolvers;
using System;
using System.Collections.Generic;


namespace ClearBank.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IDataStore _dataStore;
        private readonly IPaymentValidatorResolver _paymentValidatorResolver;

        public PaymentService(IDataStoreFactory dataStoreFactory, IPaymentValidatorResolver paymentValidatorResolver)
        {
            _dataStore = dataStoreFactory.GetDataStore();
            _paymentValidatorResolver = paymentValidatorResolver;
        }
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            try
            {
                var paymentValidator = _paymentValidatorResolver.GetPaymentValidator(request.PaymentScheme);
                var account = _dataStore.GetAccount(request.DebtorAccountNumber) 
                                    ?? throw new KeyNotFoundException("Account not found"); 
                var isValidPayment = paymentValidator.ValidatePayment(account, request);

                if (isValidPayment)
                {
                    account.Balance -= request.Amount;
                    _dataStore.UpdateAccount(account);
                }

                return new MakePaymentResult { Success = isValidPayment};
            }
            catch(Exception ex)
            {
                //log exception or/and do whatever business rules required in case of exception when creating payment
                return new MakePaymentResult { Success = false };
            }
           
        }
    }
}
