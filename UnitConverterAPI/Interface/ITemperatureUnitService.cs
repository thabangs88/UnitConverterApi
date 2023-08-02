using System.Threading.Tasks;
using UnitConverterAPI.Model;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPI.Interface
{
    public interface ITemperatureUnitService
    {
        #region Fahrenheit
        Task<(ResponseResult response, ConversionModel model)> ConvertFromFahrenheitToCelsius(double value);

        #endregion

        #region Celsius
        Task<(ResponseResult response, ConversionModel model)> ConvertFromCelsiusToFahrenheit(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromCelsiusToKelvin(double value);

        #endregion

        #region Kelvin
        Task<(ResponseResult response, ConversionModel model)> ConvertFromKelvinToCelsius(double value);

        #endregion

    }
}
