using Microsoft.AspNetCore.Identity;

namespace SoftwareSellApp.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
