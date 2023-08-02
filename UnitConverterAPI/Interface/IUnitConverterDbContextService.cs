using Microsoft.EntityFrameworkCore;
using UnitConverterAPI.Model.DbContext;

namespace UnitConverterAPI.Interface
{
    public interface IUnitConverterDbContextService
    {
         DbSet<TemperatureConverterModel> TemperatureConverterDefinition { get; set; }
         DbSet<WeightConverterModel> WeightConverterDefinition { get; set; }
         DbSet<LengthConverterModel> LengthConverterDefinition { get; set; }
         DbSet<TemperatureLoggerModel> ConverterLogger { get; set; }
    }
}
