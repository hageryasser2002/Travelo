using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelo.Application.Common.Responses;
using Travelo.Application.DTOs.Review;
using Travelo.Application.Interfaces;

namespace Travelo.Application.UseCases.Review
{
    public class GetHotelReviewsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetHotelReviewsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GenericResponse<HotelReviewDto>> GetHotelReview(
            int hotelId,
            int? pageNum = 1,
            int? pageSize = 5)
        {
            var allReviewsResponse = await _unitOfWork.Reviews.GetHotelReviews(hotelId, null, null);
            var allReviews = allReviewsResponse.Data;

            if (allReviews == null || !allReviews.Any())
            {
                return GenericResponse<HotelReviewDto>.SuccessResponse(
                     new HotelReviewDto(),
                     "No reviews found for this hotel yet."
                     );
            }

            var pagedReviewsResponse = await _unitOfWork.Reviews.GetHotelReviews(hotelId, pageNum, pageSize);
            var pagedReviews = pagedReviewsResponse.Data;

            var hotelReviewDto = new HotelReviewDto
            {
                HotelId = hotelId,
                TotalReviews = allReviews.Count,

                AvgOverallRate = Math.Round(allReviews.Average(r => r.OverallRating), 1),
                AvgValueRate = CalculateAverage(allReviews, r => r.ValueRating),
                AvgLocationRate = CalculateAverage(allReviews, r => r.LocationRating),
                AvgCleanlinessRate = CalculateAverage(allReviews, r => r.CleanlinessRating),
                AvgAmenityRate = CalculateAverage(allReviews, r => r.AmenityRating),
                AvgCommunicationRate = CalculateAverage(allReviews, r => r.CommunicationRating),


                Star5Count = allReviews.Count(r => Math.Round(r.OverallRating) == 5),
                Star4Count = allReviews.Count(r => Math.Round(r.OverallRating) == 4),
                Star3Count = allReviews.Count(r => Math.Round(r.OverallRating) == 3),
                Star2Count = allReviews.Count(r => Math.Round(r.OverallRating) == 2),
                Star1Count = allReviews.Count(r => Math.Round(r.OverallRating) == 1),

                Reviews = pagedReviews.Select(r => new ReviewDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    UserName = r.User?.UserName ?? "Guest",                   
                    HotelId = r.HotelId,
                    HotelName = r.Hotel?.Name,
                    OverallRate = r.OverallRating,
                    CleanlinessRate = r.CleanlinessRating,
                    LocationRate = r.LocationRating,
                    ValueRate = r.ValueRating,
                    AmenityRate = r.AmenityRating,

                    CommunicationRate = r.CommunicationRating,
                    Comment = r.Comment,
                    Status = r.Status,
                    CreatedAt = r.CreatedOn ?? DateTime.UtcNow
                }).ToList()
            };

            return GenericResponse<HotelReviewDto>.SuccessResponse(hotelReviewDto, "Reviews retrieved successfully");

        }
        private decimal CalculateAverage<T>(List<T> reviews, Func<T, decimal?> selector)
        {
            var values = reviews.Select(selector).Where(v => v.HasValue).Select(v => v.Value);
            return values.Any() ? Math.Round(values.Average(), 1) : 0;
        }
    }
}