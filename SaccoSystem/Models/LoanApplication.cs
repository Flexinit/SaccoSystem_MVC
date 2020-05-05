using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaccoSystem.Models
{
    public class LoanApplication
    {
        public int ID { get; set; }

        [Editable(false)]
        public string LoanRefID { get; set; } 
        public string EmployeeNo { get; set; }
        public string EmployerName { get; set; }
        public string CurrentSafPhoneNo { get; set; }
        public string DeductionAccnt { get; set; }
        //   public DateTime DateOfBirth { get; set; }
        [Editable(false)]
        public double LoanValue { get; set; }
        public string Product { get; set; }
        public string LoanPeriod { get; set; }
        public double Instalment { get; set; }
        public bool LoanSuccess { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime DateOfApplication { get; set; }
        public string PeriodPaid { get; set; }
        public string TermRemContractual { get; set; }
        public double InstalAmntPaid { get; set; }
        public double TotalArrears { get; set; }
        public bool Approved { get; set; }

    }
}