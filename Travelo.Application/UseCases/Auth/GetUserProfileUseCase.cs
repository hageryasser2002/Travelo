using Travelo.Application.Common.Responses;
using Travelo.Application.DTOs.Auth;
using Travelo.Application.Interfaces;

namespace Travelo.Application.UseCases.Auth
{
    public class GetUserProfileUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserProfileUseCase (IUnitOfWork unitOfWork)
        {
            _unitOfWork=unitOfWork;
        }
        public async Task<GenericResponse<UserProfileDTO>> GetUserData (string userId)
        {
            return await _unitOfWork.Auth.GetUserData(userId);
        }
    }
}
