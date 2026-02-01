using Travelo.Application.Common.Responses;
using Travelo.Application.DTOs.Auth;
using Travelo.Application.Interfaces;
using Travelo.Application.Services.FileService;

public class UpdateUserProfileUseCase
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFileServices fileServices;

    public UpdateUserProfileUseCase (IUnitOfWork unitOfWork, IFileServices fileServices)
    {
        this.unitOfWork=unitOfWork;
        this.fileServices=fileServices;
    }

    public async Task<GenericResponse<string>> ExecuteAsync (updateUserProfileDTO dto, string userId)
    {
        var updateData = new updateUserProfileDTORes
        {
            UserName=dto.UserName,
            FullName=dto.FullName,
            PhoneNumber=dto.PhoneNumber,
            DateOfBirth=dto.DateOfBirth,
            Address=dto.Address
        };

        if (dto.ProfilePictureUrl!=null)
        {
            var imageUploadResult = await fileServices.UploadFileAsync(dto.ProfilePictureUrl, "profile-pictures");
            if (string.IsNullOrEmpty(imageUploadResult))
            {
                return GenericResponse<string>.FailureResponse("Profile picture upload failed.");
            }

            updateData.ProfilePictureUrl=imageUploadResult;
        }

        return await unitOfWork.Auth.UpdateUserProfileAsync(updateData, userId);
    }
}
