using ClearBank.Application.DTOs;
using ClearBank.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClearBank.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public TestController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Post()
        {
            var testRequest = new MakePaymentRequest
            {
                Amount = 100,
                PaymentScheme = Common.Enums.PaymentScheme.FasterPayments,
                DebtorAccountNumber = "12345678",
                CreditorAccountNumber = "87654321",
                PaymentDate = System.DateTime.Now
            };
            var result = _paymentService.MakePayment(testRequest);
            return Ok(result);
        }
    }
}
