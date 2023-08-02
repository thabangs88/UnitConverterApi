using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UnitConverterAPI.Model.Request;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPI.Interface
{
    public interface IAuthorizationsService
    {
        Task<(ResponseResult response, TokenResponse model)> Token([FromBody] TokenRequest request);

        Task<TokenResponse> test();
    }
}
