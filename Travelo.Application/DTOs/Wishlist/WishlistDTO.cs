namespace Travelo.Application.DTOs.Wishlist
{
    public class WishlistDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<WishlistItemDTO> Items { get; set; } = new();
    }

}
