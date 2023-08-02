using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitConverterAPI.Model.DbContext;
using UnitConverterAPI.Model.Request;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPITests1.Fixtures
{
    public static class Fixtures
    {
        public static TokenRequest User() =>
            new()
             {
                  Username = "thabangtest",
                  Password = "test"

            };


        public static List<UserModel> GetUsers() =>
           new()
           {
            new UserModel()
            {
                FirstName = "Thabang",
                Username = "thabangtest",
                Password = "test",
                ID = 1,
                AppID = 1,
                CompanyID = 1,
                LastName= "Sibanyoni",
                Active = true
            }
           };


        public static TokenResponse GetTokens() =>
        new()
        {
            AccessToken = "asdasdasd9786213982173213sajdas98d72134",
            ExpiresIn = 30,
            TokenType = "Bearer"
        };

        public static (ResponseResult response, TokenResponse model) Token =>
            new()
            {
                model = GetTokens(),
                response = new ResponseResult()
                {
                    Success = true,
                    ErrorMessage = string.Empty
                }
            };
    }
}
