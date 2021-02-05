using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_eOnlineCarShop.EF_Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
        
        [ForeignKey("Gender")]
        public int GenderID { get; set; }
        public Gender Gender { get; set; }
        public Administrator Administrator { get; set; }
        public Employee Employee { get; set; }
        public Client Client { get; set; }
    }
}
