using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LoanCalc.Models
{
	public class Loan
	{
		public Loan(string name, decimal originalAmountRemaining, decimal monthlyPayment, decimal interestRate, decimal extraPaymentAmount, DateTime monthlyPaymentDate, bool includeInSnowball = true)
		{
			Id = Guid.NewGuid();
			Name = name;
			Original_AmountRemaining = originalAmountRemaining;
			MonthlyPayment = monthlyPayment;
			InterestRate = interestRate;
			ExtraPaymentAmount = extraPaymentAmount;
			MonthlyPaymentDate = monthlyPaymentDate;
			IncludeInSnowball = includeInSnowball;

			Payments = new List<Payment>();
		}

		public Guid Id { get; private set; }
		public string Name { get; set; }
		public bool IncludeInSnowball { get; set; }

		public decimal Original_AmountRemaining { get; set; }
		public decimal Current_AmountRemaining { get; set; }
		public decimal MonthlyPayment { get; set; }
		public decimal InterestRate { get; set; }
		public decimal ExtraPaymentAmount { get; set; }
		public decimal SnowballPaymentAmount { get; set; }
		public DateTime MonthlyPaymentDate { get; set; }
		public decimal PayoffMonthlyPaymentExtra { get; set; }

		public DateTime Original_ProjectedPayOffDate { get; set; }
		public DateTime New_ProjectedPayoffDate { get; set; }
		public int MonthsToPayOff { get; set; }
		public int TotalMonthsSnowballed { get; set; }
		public int MonthsPaid { get; set; }

		public List<Payment> Payments { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine(new String('-', 200));
			sb.AppendLine($@"Loan: {Name}");
			sb.AppendLine(new String('-', 200));
			sb.AppendLine($"Current Amount Remaining: {string.Format("{0:c}", Current_AmountRemaining)}");
			sb.AppendLine();
			sb.AppendLine($"Regular Monthly Payment: {string.Format("{0:c}", MonthlyPayment)}");
			sb.AppendLine($"Extra Payment: {string.Format("{0:c}", ExtraPaymentAmount)}");
			sb.AppendLine($"Total Monthly Payment: {string.Format("{0:c}", MonthlyPayment + ExtraPaymentAmount)}");
			sb.AppendLine();
			sb.AppendLine($"New Projected Payoff: {New_ProjectedPayoffDate.ToShortDateString()}");
			sb.AppendLine();
			sb.AppendLine($"Months to Payoff: {MonthsToPayOff}");
			var yearsToPayoff = MonthsToPayOff > 0 ? MonthsToPayOff / 12 > 1 ? MonthsToPayOff / 12 : 0 : 0;
			if (yearsToPayoff > 0)
				sb.AppendLine($"Years to Payoff: {yearsToPayoff}");

			sb.AppendLine();
			sb.AppendLine($"Months Paid to RunDate: {MonthsPaid}");

			sb.AppendLine();
			sb.AppendLine(new String('-', 150));
			sb.AppendLine($@"Payment Schedule");
			sb.AppendLine(new String('-', 150));
			foreach (var payment in Payments)
				sb.AppendLine($"Date {payment.Date.ToString("MM/dd/yyyy")} | Computed Loan Amount with Interest: {string.Format("{0:c}", payment.LoanAmount)} | Payment Amount: {string.Format("{0:c}", payment.Amount)} | Principal {string.Format("{0:c}", payment.Amount - payment.Interest)} | Interest {string.Format("{0:c}", payment.Interest)} ");
			return sb.ToString();
		}
	}
}