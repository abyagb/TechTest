using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;
using Shouldly;

namespace ClearBank.Tests.UnitTests.PaymentValidatorTests
{
    public class BacsPaymentValidatorTest
    {
        private Account _testAccount;
        private readonly BacsPaymentValidator _bacsPaymentValidator;
        

        public BacsPaymentValidatorTest()
        {
            _testAccount = new Account();
            _bacsPaymentValidator = new BacsPaymentValidator();
        }

        [Fact]
        public void ShouldReturnFalse_When_Account_DoesNotAllowBacsPayments()
        {
            //Arrange
            _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;

            //Act
            var result = _bacsPaymentValidator.ValidatePayment(_testAccount);

            //Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void ShouldReturnTrue_When_Account_AllowBacsPayments()
        {
            //Arrange
            _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;

            //Act
            var result = _bacsPaymentValidator.ValidatePayment(_testAccount);

            //Assert
            result.ShouldBeTrue();
        }
    }
}
