using ClearBank.Application.DTOs;
using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;
using Shouldly;

namespace ClearBank.Tests.UnitTests.PaymentValidatorTests
{
    public class FasterPaymentsValidatorTest
    {
            private Account _testAccount;
            private MakePaymentRequest _testMakePaymentRequest;
            private readonly FasterPaymentsValidator _fasterPaymentsValidator;


            public FasterPaymentsValidatorTest()
            {
                _testAccount = new Account { Balance = 500 };
                _testMakePaymentRequest = new MakePaymentRequest { Amount = 30 };
                _fasterPaymentsValidator = new FasterPaymentsValidator();
            }

            [Fact]
            public void ShouldReturnFalse_When_Account_DoesNotAllowFasterPayments()
            {
                //Arrange
                _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

                //Act
                var result = _fasterPaymentsValidator.ValidatePayment(_testAccount, _testMakePaymentRequest);

                //Assert
                result.ShouldBeFalse();
            }

            [Fact]
            public void ShouldReturnTrue_When_Account_AllowsFasterPayments()
            {
                //Arrange
                _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;

                //Act
                var result = _fasterPaymentsValidator.ValidatePayment(_testAccount, _testMakePaymentRequest);

                //Assert
                result.ShouldBeTrue();
            }

            [Fact]
            public void ShouldReturnFalse_When_Account_InsufficientFunds()
            {
                //Arrange
                _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
                _testMakePaymentRequest.Amount = 600;

                //Act
                var result = _fasterPaymentsValidator.ValidatePayment(_testAccount, _testMakePaymentRequest);

                //Assert
                result.ShouldBeFalse();
            }
        }
}
