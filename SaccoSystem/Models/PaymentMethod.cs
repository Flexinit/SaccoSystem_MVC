using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaccoSystem.Models
{
    public class PaymentMethod
    {
        public int ID { get; set; }
        public string MethodID { get; set; }
        public string MethodName { get; set; }
    }
}