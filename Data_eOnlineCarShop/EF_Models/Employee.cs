using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data_eOnlineCarShop.EF_Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string JMBG { get; set; }
        public int Experince { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
    }
}
