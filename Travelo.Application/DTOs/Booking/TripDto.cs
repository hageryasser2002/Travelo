using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelo.Domain.Models.Enums;

namespace Travelo.Application.DTOs.Booking
{
    public class TripDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public BookingType Type { get; set; }

        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public BookingStatus Status { get; set; }

        public decimal Price { get; set; }
    }

}
