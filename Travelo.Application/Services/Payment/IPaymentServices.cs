using Travelo.Application.Common.Responses;
using Travelo.Application.DTOs.Payment;

namespace Travelo.Application.Services.Payment
{
    public interface IPaymentServices
    {
        Task<GenericResponse<DTOs.Payment.RoomBookingPaymentRes>> CreateRoomBookingPayment (DTOs.Payment.RoomBookingPaymentReq req, string userId);
        Task<GenericResponse<RoomBookingPaymentRes>>? HandelSuccessAsync (int paymentId);


    }
}
