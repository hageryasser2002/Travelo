using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelo.Domain.Models.Enums;

namespace Travelo.Application.DTOs.Booking
{
   public class CreateGeneralBookingDto
   {
        public BookingType Type { get; set; }

        public int? FlightId { get; set; }
        public int? HotelId { get; set; }
        public int? RoomId { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
   }

}
