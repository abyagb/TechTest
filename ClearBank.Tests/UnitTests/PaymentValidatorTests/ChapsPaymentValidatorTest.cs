using ClearBank.Application.PaymentValidators;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;
using Shouldly;

namespace ClearBank.Tests.UnitTests.ValidatorTests
{
    public class ChapsPaymentValidatorTest
    {
        private Account _testAccount;
        private readonly ChapsPaymentValidator _chapsPaymentValidator;


        public ChapsPaymentValidatorTest()
        {
            _testAccount = new Account();
            _chapsPaymentValidator = new ChapsPaymentValidator();
        }

        [Fact]
        public void ShouldReturnFalse_When_Account_DoesNotAllowChapsPayments()
        {
            //Arrange
            _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs;
            _testAccount.Status = AccountStatus.Live;

            //Act
            var result = _chapsPaymentValidator.ValidatePayment(_testAccount);

            //Assert
            result.ShouldBeFalse();
        }

        [Fact]
        public void ShouldReturnTrue_When_Account_AllowsChapsPayments()
        {
            //Arrange
            _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _testAccount.Status = AccountStatus.Live;

            //Act
            var result = _chapsPaymentValidator.ValidatePayment(_testAccount);

            //Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void ShouldReturnFalse_When_Account_IsNotLive()
        {
            //Arrange
            _testAccount.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
            _testAccount.Status = AccountStatus.Disabled;

            //Act
            var result = _chapsPaymentValidator.ValidatePayment(_testAccount);

            //Assert
            result.ShouldBeFalse();
        }
    }
}
