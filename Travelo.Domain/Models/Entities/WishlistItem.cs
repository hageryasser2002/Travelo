using Travelo.Domain.Shared;

namespace Travelo.Domain.Models.Entities
{
    public class WishlistItem : BaseEntity
    {
        public int WishlistId { get; set; }
        public virtual Wishlist Wishlist { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}
