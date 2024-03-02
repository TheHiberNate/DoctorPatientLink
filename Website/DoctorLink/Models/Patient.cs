using System.ComponentModel;
using System.ComponentModel.DataAnnotations; // for entity framework

namespace DoctorLink.Models
{
    public class Patient
    {
        [Key] //sets Id as primary key (entity fram. takes care of this, instead of SQL statements)
        public int Id { get; set; }

        [Required] //sets Name as a required property
        [DisplayName("First Name")]
        [StringLength(200, ErrorMessage = "Maxium length is 200 caracters")]

        public string FirstName { get; set; }

        [Required] //sets Name as a required property
        [DisplayName("Last Name")]
        [StringLength(200, ErrorMessage = "Maxium length is 200 caracters")]
        public string LastName { get; set; }

        public List<Medication>? Medications { get; set; }


        //public DateTime CreatedDateTime { get; set; } = DateTime.Now; // whnver create new object, variable is set to DateTime.Now
    }
}
