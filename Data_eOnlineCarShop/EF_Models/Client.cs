using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_eOnlineCarShop.EF_Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
    }
}
