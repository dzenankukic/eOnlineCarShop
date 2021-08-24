using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eOnlineCarShop.ViewModels
{
    public class SMSMessageVM
    {
        [Display(Name = "Admin kontakt")]
        public string To { get; set; }
        [Display(Name = "Text poruke")]
        public string ContentMsg { get; set; }
    }
}
