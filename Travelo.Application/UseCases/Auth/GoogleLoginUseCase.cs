using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Travelo.Application.DTOs.Auth;
using Travelo.Application.Interfaces;

namespace Travelo.Application.UseCases.Auth
{
    public class GoogleLoginUseCase
    {
        private readonly IAuthRepository _authRepository;

        public GoogleLoginUseCase(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<AuthDTO> ExecuteAsync(GoogleLoginDTO dto)
        {
            return await _authRepository.GoogleLoginAsync(dto);
        }
    }
}

