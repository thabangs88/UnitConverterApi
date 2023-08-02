using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UnitConverterAPI.Context;
using UnitConverterAPI.Extensions;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Model.Request;
using UnitConverterAPI.Model.Response;
using UnitConverterAPI.Options;

namespace UnitConverterAPI.Service
{
    public class AuthorizationService : IAuthorizationsService
    {
        private TokenOptions _tokenOptions;
        private readonly ILogger<AuthorizationService> _logger;
        private readonly IAuthenticationDbContextService _context;

        public AuthorizationService(ILogger<AuthorizationService> logger, 
            IOptions<TokenOptions> tokenOptions,
            IAuthenticationDbContextService authenticationContext)
        {
            _logger = logger;
            _context = authenticationContext;
            _tokenOptions = tokenOptions.Value;
        }

        public async Task<TokenResponse> test()
        {
            return new TokenResponse()
            {
                AccessToken = "tesatefasdasdasd",
                TokenType = "Thabang"
            };
        }

        public async Task<(ResponseResult response, TokenResponse model)> Token([FromBody] TokenRequest request)
        {
			try
			{
                if (request == null)
                {
                    return  (new ResponseResult() { Success = false, ErrorMessage = "Object cannot be empty"}, new TokenResponse());
                }

                var user = _context.User.Find(request.Username);

                if (user == null)
                {
                    var errMessage = "User does not exists, could not process request";
                    _logger.LogError(errMessage);

                    return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
                }
                else 
                {
                    
                    if ((user.Active) && (user.Password == request.Password))
                    {
                        var company = _context.Companies.Find(user.CompanyID);
                        var app = _context.Apps.Find(user.AppID);

                        if ((company == null) || (app == null))
                        {
                            var errMessage = "Could not process request, user infornation is invalid";
                            _logger.LogError(errMessage);

                            return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
                        }
                        else
                        {
                            if ((company.Active) && (app.Active))
                            {
                                var claims = new[] {
                                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                                    new Claim(JwtRegisteredClaimNames.GivenName, company.Name)
                                };

                                var token = new JwtSecurityToken(
                                    audience: _tokenOptions.Audience,
                                    issuer: _tokenOptions.Issuer,
                                    claims: claims,
                                    expires: _tokenOptions.GetExpiration(),
                                    signingCredentials: _tokenOptions.GetSigningCredentials());

                                TokenResponse tokenResponse = new TokenResponse
                                {
                                    TokenType = _tokenOptions.Type,
                                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                    ExpiresIn = (int)_tokenOptions.ValidFor.TotalSeconds
                                };

                                return ((new ResponseResult() { Success = true }, tokenResponse));
                            }
                            else
                            {
                                var errMessage = "Could not process request, User is not active";
                                _logger.LogError(errMessage);

                                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
                            }
                        }
                    }
                    else
                    {
                        var errMessage = "User does not exists, could not process request";
                        _logger.LogError(errMessage);

                        return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
                    }
                }
                
            }
			catch (Exception ex)
			{
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(ex?.Message);
                sb.AppendLine(ex.InnerException?.Message);
                sb.AppendLine(ex?.StackTrace);

                var errMessage = sb.ToString();
                _logger.LogError(errMessage);
                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
            }
        }
    }
}
