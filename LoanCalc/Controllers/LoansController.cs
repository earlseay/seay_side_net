using LoanCalc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanCalc.Controllers
{
	public class LoansController : Controller
	{
		// GET: Loan/Snowball
		public ActionResult Snowball()
		{
			var ls = new LoanSet()
			{
				RunDate = DateTime.Now,
			};

			ls.Loans = new List<Loan>
			{
				new Loan("EJ Nelnet", 565.12m, 66.64m, 6.050m, 85m, new DateTime(2017,11,18)),
				new Loan("EJ Navient", 735.39m, 54.40m, 6.800m, 0m, new DateTime(2017,10,23)),
				new Loan("B Navient 2", 829.46m, 51.79m, 6.800m, 0m, new DateTime(2017,11,15)),
				new Loan("B Nelnet 2", 1339.27m, 67.44m, 6.550m, 0m, new DateTime(2017,11,18)),
				new Loan("VACU CC", 2960.54m, 102m, 10.990m, 0m, new DateTime(2017,11,18)),
				new Loan("JRAC HVAC", 6580m, 125m, 0m, 0m, new DateTime(2017,11,08)),
				new Loan("Durango", 24702.61m, 543.95m, 3.700m, 0m, new DateTime(2017,10,31)),
				new Loan("Discover Debt Consolidation", 29785.17m, 583.42m, 13.990m, 0m, new DateTime(2017,10,20)),
				new Loan("Mortgage", 183880.32m, 876.26m, 3.750m, 0m, new DateTime(2017,11,01)),
			};

			Loan previousLoan = null;
			//for each loan
			foreach (var loan in ls.Loans.OrderBy(al => al.Original_AmountRemaining))
			{
				if (previousLoan != null)
					loan.ExtraPaymentAmount = previousLoan.MonthlyPayment + previousLoan.ExtraPaymentAmount;
				//compute next payment date
				var date = loan.MonthlyPaymentDate;
				decimal computedAmountRemaining = loan.Original_AmountRemaining;

				//compute snowball payment (including interest
				while (computedAmountRemaining > 0)
				{
					var currentAmountRemaining = computedAmountRemaining;
					var monthNumber = ((loan.MonthlyPaymentDate.Month + loan.MonthsToPayOff) % 12);
					var interestDays = DateTime.DaysInMonth(
										(loan.MonthlyPaymentDate.Year + (ls.RunDate.Year - loan.MonthlyPaymentDate.Year)),
										monthNumber > 0 ? monthNumber : 12);
					for (int j = 0; j < interestDays; j++)
					{
						date = date.AddDays(1);
						computedAmountRemaining += computedAmountRemaining * (((loan.InterestRate / 12) / interestDays) / 100);
						if (date.Date <= ls.RunDate.Date)
							loan.Current_AmountRemaining = computedAmountRemaining;
					}
					var payment = new Payment()
					{
						Date = date,
						Amount = loan.MonthlyPayment,
						Interest = computedAmountRemaining - currentAmountRemaining,
						LoanAmount = computedAmountRemaining,
					};

					if (previousLoan == null || loan.MonthsToPayOff >= previousLoan.MonthsToPayOff)
						payment.Amount += loan.ExtraPaymentAmount;

					if (previousLoan != null && loan.MonthsToPayOff == previousLoan.MonthsToPayOff)
						payment.Amount += previousLoan.PayoffMonthlyPaymentExtra;

					//when the amount left is smaller than the total payment we were going to make, reset
					if (computedAmountRemaining <= payment.Amount)
					{
						loan.PayoffMonthlyPaymentExtra = payment.Amount - computedAmountRemaining;
						payment.Amount = computedAmountRemaining;
					}
					computedAmountRemaining -= payment.Amount;

					if (date.Date <= ls.RunDate.Date)
					{
						loan.Current_AmountRemaining = computedAmountRemaining;
						loan.MonthsPaid++;
					}
					else
					{
						loan.Payments.Add(payment);
					}
					loan.MonthsToPayOff++;
				}
				loan.New_ProjectedPayoffDate = date;
				previousLoan = loan;
			}

			ls.Loans.RemoveAll(l => l.Current_AmountRemaining <= 0);
			return View(ls);
		}

		[HttpPost]
		public ActionResult Submit(LoanSet model)
		{
			return View("Snowball", model);
		}
	}
}