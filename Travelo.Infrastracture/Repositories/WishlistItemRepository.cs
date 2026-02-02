using Travelo.Application.Interfaces;
using Travelo.Domain.Models.Entities;
using Travelo.Infrastracture.Contexts;

namespace Travelo.Infrastracture.Repositories
{
    public class WishlistItemRepository : GenericRepository<WishlistItem>, IWishlistItemRepository
    {
        public WishlistItemRepository (ApplicationDbContext _context) : base(_context)
        {
        }
    }
}
