﻿using Taskify.API.Models;
using Taskify.API.Models.Dtos;

namespace Taskify.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(UserDto request);
        Task<string?> LoginAsync(UserDto request);
    }
}
