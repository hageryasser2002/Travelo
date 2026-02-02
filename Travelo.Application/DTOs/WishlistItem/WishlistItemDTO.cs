public class WishlistItemDTO
{
    public int Id { get; set; }
    public int HotelId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public double Rating { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int ReviewsCount { get; set; }

}