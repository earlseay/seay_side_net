using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalc.Models
{
	public class LoanSet
	{
		public DateTime RunDate { get; set; }
		public List<Loan> Loans { get; set; }
	}
}