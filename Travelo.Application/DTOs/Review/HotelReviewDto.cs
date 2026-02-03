using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelo.Application.DTOs.Review
{
    public class HotelReviewDto
    {
        public int HotelId { get; set; }
        public int TotalReviews { get; set; }
        public decimal AvgOverallRate { get; set; }
        public decimal AvgCleanlinessRate { get; set; }
        public decimal AvgLocationRate { get; set; }
        public decimal AvgValueRate { get; set; }
        public decimal AvgCommunicationRate { get; set; }
        public decimal AvgAmenityRate { get; set; }

        public int Star5Count { get; set; }
        public int Star4Count { get; set; }
        public int Star3Count { get; set; }
        public int Star2Count { get; set; }
        public int Star1Count { get; set; }

        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();

    }
}
