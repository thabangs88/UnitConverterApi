using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitConverterAPI.Model.DbContext
{
    [Table(nameof(TemperatureConverterModel), Schema = "dbo")]
    public class LengthConverterModel
    {
        [Key]
        public int Id { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Fomulae { get; set; }
        public string Unit { get; set; }
    }
}
