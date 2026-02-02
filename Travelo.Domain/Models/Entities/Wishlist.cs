using Travelo.Domain.Shared;

namespace Travelo.Domain.Models.Entities
{
    public class Wishlist : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Title { get; set; }

        public virtual ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
    }
}
