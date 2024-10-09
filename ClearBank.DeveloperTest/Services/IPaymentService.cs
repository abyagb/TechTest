using ClearBank.Application.DTOs;

namespace ClearBank.Application.Services
{
    public interface IPaymentService
    {
        MakePaymentResult MakePayment(MakePaymentRequest request);
    }
}
