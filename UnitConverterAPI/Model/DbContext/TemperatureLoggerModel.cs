using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitConverterAPI.Model.DbContext
{
    [Table(nameof(UserModel), Schema = "dbo")]
    public class TemperatureLoggerModel
    {
        [Key]
        public int Id { get; set; }
        public string User { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public double Result { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
