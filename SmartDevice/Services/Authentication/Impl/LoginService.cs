using SmartDevice.Dto;
using SmartDevice.Models;
using SmartDevice.Repositories;
using SmartDevice.Services.Authentication.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SmartDevice.Services.Authentication.Impl
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;
        private SigningConfiguration signingConfiguration;
        private TokenConfiguration tokenConfiguration;
        private const string Login = "Login";
        private const string MSG_USER_PASSWORD_INVALID = "User or password invalid";

        public LoginService(IUserRepository userRepository, SigningConfiguration signingConfiguration, TokenConfiguration tokenConfiguration)
        {
            this.userRepository = userRepository;
            this.signingConfiguration = signingConfiguration;
            this.tokenConfiguration = tokenConfiguration;
        }

        public async Task<AuthenticateResponse> Authenticate(LoginDto loginVO, string ipAddress)
        {
            bool credentialIsValid = false;
            User baseUser = await userRepository.FindByLogin(loginVO.Login);
            credentialIsValid = (baseUser != null &&
                                loginVO.Login.Equals(baseUser.Login) &&
                                baseUser.Password.Equals(loginVO.Password));

            if (credentialIsValid)
            {
                return GenerateSucessResponseWithTokens(baseUser, ipAddress);
            }
            else
            {
                return new AuthenticateResponse(false, ExceptionObject(MSG_USER_PASSWORD_INVALID));
            }
        }

        private AuthenticateResponse GenerateSucessResponseWithTokens(User user, string ipAddress)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                                new GenericIdentity(user.Login, Login),
                                new[]
                                {
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Login)
                                }
                            );


            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(tokenConfiguration.Seconds);

            //Responsible to handle the security of the token
            var handler = new JwtSecurityTokenHandler();
            string token = CreateJwtToken(identity, createDate, expirationDate, handler);
            return new AuthenticateResponse(true, SuccessObject(createDate, expirationDate, tokenConfiguration.Seconds, token));
        }

        private string CreateJwtToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = tokenConfiguration.Issuer,
                Audience = tokenConfiguration.Audience,
                SigningCredentials = signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, int timeInSeconds, string token)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiresIn = timeInSeconds,
                accessToken = token,
                message = "Ok"
            };
        }

        private object ExceptionObject(string msg)
        {
            return new
            {
                authenticated = false,
                message = msg
            };
        }
    }
}
