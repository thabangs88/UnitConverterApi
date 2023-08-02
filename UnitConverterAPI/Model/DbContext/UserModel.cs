using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitConverterAPI.Model.DbContext
{
    [Table(nameof(UserModel), Schema = "dbo")]
    public partial class UserModel
    {

        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public int CompanyID { get; set; }
        public int AppID { get; set; }
        public bool Active { get; set; }
    }
}
