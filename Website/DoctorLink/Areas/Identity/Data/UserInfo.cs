using Microsoft.AspNetCore.Identity;

namespace DoctorLink.Areas.Identity.Data
{
    public class UserInfo : IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        //public string PhoneNumber {  get; set; }
        // other stuff here
    }
}
