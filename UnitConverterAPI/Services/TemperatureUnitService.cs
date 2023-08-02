using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitConverterAPI.Context;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Model.DbContext;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPI.Service
{
    public class TemperatureUnitService: ITemperatureUnitService
    {
        private readonly ILogger<TemperatureUnitService> _logger;
        IHttpContextAccessor _httpContextAccessor;
        private readonly UnitConverterDbContext _unitConverterDb;
        public TemperatureUnitService(ILogger<TemperatureUnitService> logger,
                               IHttpContextAccessor httpContextAccessor,
                               UnitConverterDbContext unitConverterDb)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _unitConverterDb = unitConverterDb;
        }
        private (ResponseResult result, ConversionModel model) Convert(string convertType, double value, double result)
        {
            var converter = _unitConverterDb.TemperatureConverterDefinition.FirstOrDefault(x => x.ShortDescription == convertType);

            if (converter != null)
            {
                var responseModel = new ConversionModel()
                {
                    Description = converter.Description,
                    Value = Math.Round(value, 2),
                    ValueDescription = $"{value} {converter.Unit}"
                };

                var user = _httpContextAccessor.HttpContext.User.Identity.Name;

                _unitConverterDb.ConverterLogger.Add(new TemperatureLoggerModel()
                {
                    Description = converter.Description,
                    Result = Math.Round(value, 2),
                    TimeStamp = DateTime.Now,
                    Value = result,
                    User = user
                });

                _unitConverterDb.SaveChangesAsync();

                return ((new ResponseResult() { Success = true }, responseModel));
            }
            else
            {
                var errMessage = "Could not find Temperature Converter";
                _logger.LogError(errMessage);
                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
            }
        }

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromCelsiusToFahrenheit(double value)
        {
            try
            {
                var convertValue = (value * 1.8) + 32;

                var result = Convert("CelsiusToFahrenheit", convertValue, value);

                if(result.result.Success)
                    return await Task.FromResult(((new ResponseResult() { Success = true }, result.model)));
                else
                    return ((new ResponseResult() { Success = false, ErrorMessage = result.result.ErrorMessage }, null));
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message;
                _logger.LogError(errMessage);
                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
            }
        }

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromCelsiusToKelvin(double value)
        {
            try
            {
                var convertValue = value + 273.15;

                var result = Convert("CelsiusToKelvin", convertValue, value);

                if (result.result.Success)
                    return await Task.FromResult(((new ResponseResult() { Success = true }, result.model)));
                else
                    return ((new ResponseResult() { Success = false, ErrorMessage = result.result.ErrorMessage }, null));
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message;
                _logger.LogError(errMessage);
                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
            }
        }

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromFahrenheitToCelsius(double value)
        {
            try
            {
                var convertValue = (value - 32) / 1.8;

                var result = Convert("FahrenheitToCelsius", convertValue, value);

                if (result.result.Success)
                    return await Task.FromResult(((new ResponseResult() { Success = true }, result.model)));
                else
                    return ((new ResponseResult() { Success = false, ErrorMessage = result.result.ErrorMessage }, null));
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message;
                _logger.LogError(errMessage);

                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
            }
        }

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromKelvinToCelsius(double value)
        {
            try
            {
                var convertValue = value - 273.15;

                var result = Convert("KelvinToCelsius", convertValue, value);

                if (result.result.Success)
                    return await Task.FromResult(((new ResponseResult() { Success = true }, result.model)));
                else
                    return ((new ResponseResult() { Success = false, ErrorMessage = result.result.ErrorMessage }, null));
            }
            catch (Exception ex)
            {
                var errMessage = ex.Message;
                _logger.LogError(errMessage);
                return ((new ResponseResult() { Success = false, ErrorMessage = errMessage }, null));
            }
        }
    }
}
