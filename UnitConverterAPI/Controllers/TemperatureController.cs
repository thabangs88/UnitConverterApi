using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnitConverterAPI.Interface;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPI.Controllers
{
    [ApiController]
    [EnableCors("AllowsAll")]
    [Route("api/temperature/convert/")]
    [ApiExplorerSettings(GroupName = "Temperature Unit Converter")]
    [Authorize]
    public class TemperatureController : Controller
    {
        private readonly ITemperatureUnitService _temperatureUnitManager;
        public TemperatureController(ITemperatureUnitService temperatureUnitManager)
        {
            _temperatureUnitManager = temperatureUnitManager;
        }

        [HttpPost("CelsiusToFahrenheit")]
        public async Task<ActionResult<ConversionModel>> CelsiusToFahrenheit(double value)
        {
            var request = _temperatureUnitManager.ConvertFromCelsiusToFahrenheit(value).Result;

            if (request.response.Success)
                return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("celsiustokelvin")]
        public async Task<ActionResult<ConversionModel>> CelsiusToKelvin(double value)
        {
            var request = _temperatureUnitManager.ConvertFromCelsiusToKelvin(value).Result;

            if (request.response.Success)
                return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("fahrenheittoCelsius")]
        public async Task<ActionResult<ConversionModel>> FahrenheitToCelsius(double value)
        {
            var request = _temperatureUnitManager.ConvertFromFahrenheitToCelsius(value).Result;

            if (request.response.Success)
                return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("kelvintocelsius")]
        public async Task<ActionResult<ConversionModel>> KelvinToCelsius(double value)
        {
            var request = _temperatureUnitManager.ConvertFromKelvinToCelsius(value).Result;

            if (request.response.Success)
                return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }
    }
}
