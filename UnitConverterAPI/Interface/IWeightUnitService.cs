using System.Threading.Tasks;
using UnitConverterAPI.Model.Response;

namespace UnitConverterAPI.Interface
{
    public interface IWeightUnitService
    {
        Task<(ResponseResult response, ConversionModel model)> ConvertFromGramToKilogram(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromKilogramToGram(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromKilogramToMilligram(double value);
        Task<(ResponseResult response, ConversionModel model)> ConvertFromMilligramToKilogram(double value);
    }
}
