using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDevice.Services.Authentication
{
    public class AuthenticateResponse
    {
        public bool Authenticated { get; set; }

        public object Response { get; set; }

        public AuthenticateResponse(bool authenticated, object response)
        {
            Authenticated = authenticated;
            Response = response;
        }
    }
}
