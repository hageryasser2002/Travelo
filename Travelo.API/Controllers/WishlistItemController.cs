using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Travelo.Application.DTOs.WishlistItem;

namespace Travelo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WishlistItemController : ControllerBase
    {
        private readonly IWishlistItemService wishlistItemService;

        public WishlistItemController (IWishlistItemService wishlistItemService)
        {
            this.wishlistItemService=wishlistItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItem ([FromBody] WishlistItemReqDTO dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await wishlistItemService
                .AddWishListItem(userId, dto);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem (int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var result = await wishlistItemService
                .DeleteWishListItem(userId, id);

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
