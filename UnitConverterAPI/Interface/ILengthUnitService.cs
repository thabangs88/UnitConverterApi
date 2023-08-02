using System.Threading.Tasks;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPI.Interface
{
    public interface ILengthUnitService
    {
        Task<(ResponseResult response, ConversionModel model)> ConvertFromKilometerToMeter(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromMeterToCentimeter(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromMillimeterToCentimeter(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromCentimeterToMillimeter(double value);

    }
}

