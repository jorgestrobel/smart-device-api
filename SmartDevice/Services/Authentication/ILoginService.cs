using SmartDevice.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Services.Authentication
{
    public interface ILoginService
    {
        Task<AuthenticateResponse> Authenticate(LoginDto loginVO, string ipAddress);

    }
}
