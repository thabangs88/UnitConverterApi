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

    public class LengthUnitService: ILengthUnitService
    {
        private readonly ILogger<LengthUnitService> _logger;
        IHttpContextAccessor _httpContextAccessor;
        private readonly UnitConverterDbContext _unitConverterDb;
        public LengthUnitService(ILogger<LengthUnitService> logger,
                        IHttpContextAccessor httpContextAccessor,
                        UnitConverterDbContext unitConverterDb)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _unitConverterDb = unitConverterDb;
        }
        private (ResponseResult result, ConversionModel model) Convert(string convertType, double value, double result)
        {
            var converter = _unitConverterDb.LengthConverterDefinition.FirstOrDefault(x => x.ShortDescription.ToLower() == convertType.ToLower());

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

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromKilometerToMeter(double value)
        {
            try
            {
                var convertValue = value * 1000;

                var result = Convert("KilometerToMeter", convertValue, value);

                if (result.result.Success)
                    return await Task.FromResult(((new ResponseResult() { Success = true }, result.model)));
                else
                    return ((new ResponseResult() { Success = false, ErrorMessage = result.result.ErrorMessage }, null));
            }
            catch (Exception ex)
            {
                return ((new ResponseResult() { Success = false, ErrorMessage = ex.Message }, null));
            }
        }

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromMeterToCentimeter(double value)
        {
            try
            {
                var convertValue = value * 1000;

                var result = Convert("MeterToCentimeter", convertValue, value);

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

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromMillimeterToCentimeter(double value)
        {
            try
            {
                var convertValue = value / 10;

                var result = Convert("MillimeterToCentimeter", convertValue, value);

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

        public async Task<(ResponseResult response, ConversionModel model)> ConvertFromCentimeterToMillimeter(double value)
        {
            try
            {
                var convertValue = value * 10;

                var result = Convert("CentimeterToMillimeter", convertValue, value);

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
