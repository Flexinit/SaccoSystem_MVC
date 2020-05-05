using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaccoSystem.Models
{
    public class MemberDetails
    {

      public  int ID { get; set; }
      public  string Country { get; set; }
      public  string Branch { get; set; }
      public  string LoanRefID { get; set; }
      public  string ClientRefID { get; set; }
      public  string Surname { get; set; }
      public  string FirstName { get; set; }
      public  string IDNumber { get; set; }
      public  string EmployeeNo { get; set; }
      public  string PhoneNo { get; set; }
      public  string EmployerGroup { get; set; }
      public  string PaymentMethod { get; set; }
      public  string Product { get; set; }
    }
}