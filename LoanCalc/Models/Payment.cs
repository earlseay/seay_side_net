using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalc.Models
{
	public class Payment
	{
		public decimal Amount { get; set; }
		public decimal Interest { get; set; }
		public decimal LoanAmount { get; set; }
		public DateTime Date { get; set; }
	}
}