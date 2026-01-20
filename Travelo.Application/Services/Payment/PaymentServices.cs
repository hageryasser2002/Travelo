using Stripe.Checkout;
using Travelo.Application.Common.Responses;
using Travelo.Application.DTOs.Payment;
using Travelo.Application.Interfaces;

namespace Travelo.Application.Services.Payment
{
    public class PaymentServices : IPaymentServices
    {
        private readonly IPaymentRepository _payment;
        private readonly IHotelRepository _hotel;

        public PaymentServices (IPaymentRepository payment, IHotelRepository hotel)
        {
            _payment=payment;
            _hotel=hotel;
        }

        public async Task<GenericResponse<RoomBookingPaymentRes>> CreateRoomBookingPayment (RoomBookingPaymentReq req, string userId, string HTTPReq)
        {
            var hotel = await _hotel.GetHotelByIdAsync(req.HotelId);
            if (hotel==null)
            {
                return GenericResponse<RoomBookingPaymentRes>.FailureResponse("Hotel not found");
            }

            var payment = new Travelo.Domain.Models.Entities.Payment
            {
                UserId=userId,
                Amount=100,
                HotelId=req.HotelId,
                RoomId=req.RoomId,
                PaymentDate=DateTime.UtcNow,
                Status=PaymentStatus.Pending
            };
            await _payment.Add(payment);
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes=new List<string> { "card" },
                LineItems=new List<SessionLineItemOptions>(),
                Mode="payment",
                SuccessUrl=$"{HTTPReq}/api/User/Payment/Success/{payment.Id}",
                CancelUrl=$"{HTTPReq}/api/User/Payment/cancel",
            };

        }

        public Task<GenericResponse<RoomBookingPaymentRes>>? HandelSuccessAsync (int paymentId)
        {
            throw new NotImplementedException();
        }
    }
}
