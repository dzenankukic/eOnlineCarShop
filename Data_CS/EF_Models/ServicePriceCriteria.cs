using System;
using System.Collections.Generic;
using System.Text;

namespace Data_CS.EF_Models
{
    public class ServicePriceCriteria
    {
        public int ID { get; set; }
        public string CriteriaName { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
    }
}
