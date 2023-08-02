using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UnitConverterAPI.Model.DbContext
{
    [Table(nameof(CompanyModel), Schema = "dbo")]
    public partial class CompanyModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
