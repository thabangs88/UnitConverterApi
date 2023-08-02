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
    [Route("api/length/convert/")]
    [ApiExplorerSettings(GroupName = "Length Unit Converter")]
    [Authorize]
    public class LengthController : Controller
    {
        private readonly ILengthUnitService _lengthUnitManager;
        public LengthController(ILengthUnitService lengthUnitManager)
        {
            _lengthUnitManager = lengthUnitManager;
        }

        [HttpPost("kilometertometer")]
        public async Task<IActionResult> CelsiusToFahrenheit(double value)
        {
            var request = _lengthUnitManager.ConvertFromKilometerToMeter(value).Result;

            if (request.response.Success)
                return Ok(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("metertocentimeter")]
        public async Task<IActionResult> CelsiusToKelvin(double value)
        {
            var request = _lengthUnitManager.ConvertFromMeterToCentimeter(value).Result;

            if (request.response.Success)
                return Ok(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("millimetertocentimeter")]
        public async Task<IActionResult> FahrenheitToCelsius(double value)
        {
            var request = _lengthUnitManager.ConvertFromMillimeterToCentimeter(value).Result;

            if (request.response.Success)
                return Ok(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("centimetertomillimeter")]
        public async Task<IActionResult> CentimeterToMillimeter(double value)
        {
            var request = _lengthUnitManager.ConvertFromCentimeterToMillimeter(value).Result;

            if (request.response.Success)
                return Ok(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }
    }
}
