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
    [Route("api/weight/convert/")]
    [ApiExplorerSettings(GroupName = "Weight Unit Converter")]
    public class WeightController : Controller
    {
        private readonly IWeightUnitService _weightUnitManager;
        public WeightController(IWeightUnitService weightUnitManager)
        {
            _weightUnitManager = weightUnitManager;
        }

        [HttpPost("gramtokilogram")]
        public async Task<ActionResult<ConversionModel>> ConvertFromGramToKilogram(double value)
        {
            var request = _weightUnitManager.ConvertFromGramToKilogram(value).Result;

            if (request.response.Success)
                return Ok(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("kilogramtogram")]
        public async Task<ActionResult<ConversionModel>> ConvertFromKilogramToGram(double value)
        {
            var request = _weightUnitManager.ConvertFromKilogramToGram(value).Result;

            if (request.response.Success)
                return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("kilogramtomilligram")]
        public async Task<ActionResult<ConversionModel>> ConvertFromKilogramToMilligram(double value)
        {
            var request = _weightUnitManager.ConvertFromKilogramToMilligram(value).Result;

            if (request.response.Success)
                  return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }

        [HttpPost("milligramtokilogram")]
        public async Task<ActionResult<ConversionModel>> ConvertFromMilligramToKilogram(double value)
        {
            var request = _weightUnitManager.ConvertFromMilligramToKilogram(value).Result;

            if (request.response.Success)
                return await Task.FromResult(request.model);
            else
                return StatusCode(200, request.response.ErrorMessage);
        }
    }
}
