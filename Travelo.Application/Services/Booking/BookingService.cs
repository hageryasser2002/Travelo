using Travelo.Application.DTOs.Booking;
using Travelo.Application.Interfaces;
using Travelo.Domain.Models.Entities;
using Travelo.Domain.Models.Enums;

namespace Travelo.Application.Services.Booking
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightRepository _flightRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IFlightRepository flightRepository)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
        }

        public async Task<BookingDto> CreateFlightBookingAsync(CreateBookingDto dto)
        {
            var flight = await _flightRepository.GetByIdAsync(dto.FlightId);
            if (flight == null)
                throw new Exception("Flight not found");

            var booking = new Travelo.Domain.Models.Entities.Booking
            {
                FlightId = flight.Id,
                Status = BookingStatus.Pending,
                TotalPrice = flight.Price
            };

            await _bookingRepository.AddAsync(booking);
            await _bookingRepository.SaveChangesAsync();

            return MapToDto(booking);
        }

        public async Task<BookingDto> ConfirmBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            if (booking.Status == BookingStatus.Cancelled)
                throw new Exception("Booking is cancelled");

            if (booking.Status != BookingStatus.Pending)
                throw new Exception("Booking cannot be confirmed");


            booking.Status = BookingStatus.Confirmed;

            await _bookingRepository.SaveChangesAsync();

            return MapToDto(booking);
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            booking.Status = BookingStatus.Cancelled;
            await _bookingRepository.SaveChangesAsync();
        }

        private BookingDto MapToDto(Travelo.Domain.Models.Entities.Booking b)
        {
            return new BookingDto
            {
                Id = b.Id,
                FlightId = b.FlightId,
                Status = b.Status,
                TotalPrice = b.TotalPrice,
                TicketId = b.Ticket?.Id
            };
        }

        public async Task<TripDto> CreateAsync(
            string userId,
            CreateGeneralBookingDto dto)
        {
            var booking = new GeneralBooking
            {
                UserId = userId ,
                Type = dto.Type,

                FlightId = dto.FlightId,
                HotelId = dto.HotelId,
                RoomId = dto.RoomId,

                FromDate = dto.FromDate,
                ToDate = dto.ToDate,

                Status = BookingStatus.Pending,
                TotalPrice = 0
            };

            await _bookingRepository.AddGeneralAsync(booking);

            return MapToTripDto(booking);
        }

        public async Task<List<TripDto>> GetMyTripsAsync(string userId)
        {
            var bookings =
                await _bookingRepository.GetByUserIdAsync(userId);

            return bookings
                .Select(b => MapToTripDto(b))
                .ToList();
        }


        public async Task<BookingDetailsDto?> GetDetailsAsync(int id)
        {
            var booking =
                await _bookingRepository.GetGeneralByIdAsync(id);

            if (booking == null) return null;

            return MapToDetailsDto(booking);
        }


        public async Task CancelAsync(int id)
        {
            var booking =
                await _bookingRepository.GetByIdAsync(id);

            if (booking == null)
                throw new Exception("Not found");

            booking.Status = BookingStatus.Cancelled;

            await _bookingRepository.SaveChangesAsync();
        }

        // ===================== Mappers =====================

        private TripDto MapToTripDto(GeneralBooking b)
        {
            return new TripDto
            {
                Id = b.Id,

                Title = b.Type switch
                {
                    BookingType.Flight => b.Flight?.FromAirport + " → " + b.Flight?.ToAirport,
                    BookingType.Hotel => b.Hotel?.Name,
                    BookingType.Room => b.Room?.View,
                    _ => "Trip"
                },

                Type = b.Type,

                From = b.FromDate,
                To = b.ToDate,

                Status = b.Status,
                Price = b.TotalPrice
            };
        }

        private BookingDetailsDto MapToDetailsDto(GeneralBooking b)
        {
            return new BookingDetailsDto
            {
                Id = b.Id,

                Name = b.Type switch
                {
                    BookingType.Flight => b.Flight?.FlightNumber,
                    BookingType.Hotel => b.Hotel?.Name,
                    BookingType.Room => b.Room?.View,
                    _ => ""
                },

                Type = b.Type,

                FromDate = b.FromDate,
                ToDate = b.ToDate,

                BasePrice = b.PriceDetails?.BaseFare ?? 0,
                Taxes = b.PriceDetails?.Taxes ?? 0,
                Total = b.TotalPrice,

                Status = b.Status
            };
        }

    }
}
