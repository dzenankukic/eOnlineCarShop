using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_eOnlineCarShop.EF_Models
{
    public class Administrator
    {
        public int AdministratorID { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
