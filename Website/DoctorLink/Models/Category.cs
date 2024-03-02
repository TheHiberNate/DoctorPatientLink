using System.ComponentModel;
using System.ComponentModel.DataAnnotations; // for entity framework

namespace DoctorLink.Models
{
    public class Category
    {
        [Key] //sets Id as primary key (entity fram. takes care of this, instead of SQL statements)
        public int Id { get; set; }
        [Required] //sets Name as a required property
        public string Name { get; set; }
        [DisplayName("Display Name")]
        //[Range(1,100,ErrorMessage ="Display Order must be between 1 and 100 only!!")]
        public string DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now; // whnver create new object, variable is set to DateTime.Now
    }
}
