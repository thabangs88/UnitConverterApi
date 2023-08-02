using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UnitConverterAPI.Model.DbContext
{
    [Table(nameof(AppModel), Schema = "dbo")]
    public partial class AppModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Platform { get; set; }
        public bool Active { get; set; }
    }
}
