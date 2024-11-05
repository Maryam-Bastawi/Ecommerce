using Microsoft.AspNetCore.Identity;
using Store4.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Services.Contract
{
     public interface IUserService
    {
       Task<UserDto> LoginAsync(LoginDto loginDto);
       Task<UserDto> RegisterAsync(RegisterDto registerDto);
       Task<bool> CheckEmailExitsAsync(string email);

    }
}
