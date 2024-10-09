using AutoFixture;
using ClearBank.Application.DTOs;
using ClearBank.Application.Interfaces;
using ClearBank.Application.PaymentValidators;
using ClearBank.Application.Resolvers;
using ClearBank.Application.Services;
using ClearBank.Common.Enums;
using ClearBank.Domain.Entities;
using Moq;
using Shouldly;

namespace ClearBank.Tests.UnitTests
{
    public class PaymentServiceTest
    {
        //This constructor is doing too much. Will ideally move this out to a base IFxture<T> class
        private Mock<IDataStore> _mockDataStore;
        private readonly Mock<IDataStoreFactory> _mockDataStoreFactory;
        private readonly Mock<IPaymentValidatorResolver> _mockPaymentValidatorResolver;
        private Mock<IPaymentValidator> _mockPaymentValidator;
        private readonly Fixture _fixture;
        private Account _testAccount;
        private MakePaymentRequest _testMakePaymentRequest;
        private readonly PaymentService _paymentService;

        public PaymentServiceTest()
        {
            _mockPaymentValidator = new Mock<IPaymentValidator>();
            _mockDataStore = new Mock<IDataStore>();
            _mockPaymentValidatorResolver = new Mock<IPaymentValidatorResolver>();
            _mockDataStoreFactory = new Mock<IDataStoreFactory>();
            _mockDataStoreFactory.Setup(x => x.GetDataStore()).Returns(_mockDataStore.Object);
            _fixture = new Fixture();
            _testMakePaymentRequest = _fixture.Create<MakePaymentRequest>();
            _testAccount = _fixture.Build<Account>().With(x => x.Balance, _testMakePaymentRequest.Amount * 3).Create();
            _mockDataStore.Setup(x => x.GetAccount(_testMakePaymentRequest.DebtorAccountNumber)).Returns(_testAccount);
            _mockPaymentValidatorResolver.Setup(x => x.GetPaymentValidator(It.IsAny<PaymentScheme>())).Returns(_mockPaymentValidator.Object);
            _paymentService = new PaymentService(_mockDataStoreFactory.Object, _mockPaymentValidatorResolver.Object);
        }


        [Fact]
        public void ShouldReturnSuccessfulPayment_WhenPayment_IsValid()
        {
            //Arrange 
            _mockPaymentValidator.Setup(x => x.ValidatePayment(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(true);        

            //Act
            var makePaymentResult = _paymentService.MakePayment(_testMakePaymentRequest);

            //Assert
            makePaymentResult.Success.ShouldBeTrue();
        }

        [Fact]
        public void ShouldReturnUnSuccessfulPayment_WhenPayment_IsNotValid()
        {
            //Arrange 
            _mockPaymentValidator.Setup(x => x.ValidatePayment(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(false);

            //Act
            var makePaymentResult = _paymentService.MakePayment(_testMakePaymentRequest);

            //Assert
            makePaymentResult.Success.ShouldBeFalse();
        }

        [Fact]
        public void ShouldUpdateBankBalance_When_Payment_IsValid()
        {
            //Arrange 
            var remainingAccountBalance = _testAccount.Balance - _testMakePaymentRequest.Amount;
            _mockPaymentValidator.Setup(x => x.ValidatePayment(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(true);

            //Act
            var makePaymentResult = _paymentService.MakePayment(_testMakePaymentRequest);

            //Assert
            makePaymentResult.Success.ShouldBeTrue();
            _testAccount.Balance.ShouldBe(remainingAccountBalance);
        }

        [Fact]
        public void ShouldNotUpdateBankBalance_When_Payment_IsNotValid()
        {
            //Arrange 
            _mockPaymentValidator.Setup(x => x.ValidatePayment(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Returns(false);

            //Act
            var makePaymentResult = _paymentService.MakePayment(_testMakePaymentRequest);

            //Assert
            makePaymentResult.Success.ShouldBeFalse();
            _testAccount.Balance.ShouldBe(_testAccount.Balance);
        }

        [Fact]
        public void ShouldReturnUnsuccessfulPayment_When_PaymentValidator_DoesNotExist()
        {
            //Arrange 
            _mockPaymentValidator.Setup(x => x.ValidatePayment(It.IsAny<Account>(), It.IsAny<MakePaymentRequest>())).Throws(new ArgumentException());

            //Act
            var makePaymentResult = _paymentService.MakePayment(_testMakePaymentRequest);

            //Assert
            makePaymentResult.Success.ShouldBeFalse();
        }

        [Fact]
        public void ShouldReturnUnsuccessfulPayment_When_Account_DoesNotExist()
        {
            //Arrange 
            _mockDataStore.Setup(x => x.GetAccount(_testMakePaymentRequest.DebtorAccountNumber)).Throws(new KeyNotFoundException());

            //Act
            var makePaymentResult = _paymentService.MakePayment(_testMakePaymentRequest);

            //Assert
            makePaymentResult.Success.ShouldBeFalse();
        }
    }
}