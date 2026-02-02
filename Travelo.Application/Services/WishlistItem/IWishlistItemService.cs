using Travelo.Application.Common.Responses;
using Travelo.Application.DTOs.WishlistItem;

public interface IWishlistItemService
{
    Task<GenericResponse<string>> AddWishListItem (string userId, WishlistItemReqDTO reqDTO);

    Task<GenericResponse<string>> DeleteWishListItem (string userId, int itemId);
}
