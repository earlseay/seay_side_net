<Query Kind="Program" />

/*
A program to calculate the projected loan payoff dates.

Snowball Payment method:
	Pay extra on your smallest loan, when that loan is paid off, use the extra + the loan's payment amount to pay on the next smallest loan until all loans are paid off.
	
	var monthArray = new int[]{5,3,2,3,5,8,13,16,85};
	var date = new DateTime(2017,10,18);
	Console.Out.WriteLine("Start Month {0}", date);
	foreach (var monthCount in monthArray)
	{
		Console.Out.WriteLine("Months {0}", monthCount);
		date = date.AddMonths(monthCount);
		date.Dump();
	}
*/
#region Input Parameters
//public static DateTime RunDate = DateTime.Now;
//public static DateTime RunDate = new DateTime(2018, 5, 30);
public static DateTime RunDate = new DateTime(2017, 11, 30);

public List<Loan> AllLoans = new List<Loan>
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

/*
Current Loans
	new Loan("EJ Nelnet", 565.12m, 66.64m, 6.050m, 85m, new DateTime(2017,11,18)),
	new Loan("EJ Navient", 735.39m, 54.40m, 6.800m, 0m, new DateTime(2017,10,23)),
	new Loan("B Navient 2", 829.46m, 51.79m, 6.800m, 0m, new DateTime(2017,11,15)),
	new Loan("B Nelnet 2", 1339.27m, 67.44m, 6.550m, 0m, new DateTime(2017,11,18)),
	new Loan("VACU CC", 2960.54m, 102m, 10.990m, 0m, new DateTime(2017,11,18)),
	new Loan("JRAC HVAC", 6580m, 125m, 0m, 0m, new DateTime(2017,11,08)),
	new Loan("Durango", 24702.61m, 543.95m, 3.700m, 0m, new DateTime(2017,10,31)),
	new Loan("Discover Debt Consolidation", 29785.17m, 583.42m, 13.990m, 0m, new DateTime(2017,10,20)),
	new Loan("Mortgage", 183880.32m, 876.26m, 3.750m, 0m, new DateTime(2017,11,01)),

Other loan scenarios
	Payoff smallest loan now
	new Loan("EJ Navient", 686.08m, 54.40m, 6.800m, 151.64m, new DateTime(2017,11,23)),
	new Loan("B Navient 2", 829.46m, 51.79m, 6.800m, 0m, new DateTime(2017,11,15)),
	new Loan("B Nelnet 2", 1339.27m, 67.44m, 6.550m, 0m, new DateTime(2017,11,18)),
	new Loan("VACU CC", 2960.54m, 102m, 10.990m, 0m, new DateTime(2017,11,18)),
	new Loan("JRAC HVAC", 6580m, 125m, 0m, 0m, new DateTime(2017,11,08)),
	new Loan("Durango", 24702.61m, 543.95m, 3.700m, 0m, new DateTime(2017,10,31)),
	new Loan("Discover Debt Consolidation", 29785.17m, 583.42m, 13.990m, 0m, new DateTime(2017,10,20)),
	new Loan("Mortgage", 183880.32m, 876.26m, 3.750m, 0m, new DateTime(2017,11,01)),



	new Loan("EJ Nelnet", 565.12m, 66.64m, 6.050m, 85m, new DateTime(2017,11,18)),
	new Loan("EJ Navient", 735.39m, 54.40m, 6.800m, 0m, new DateTime(2017,10,23)),
	new Loan("B Navient 2", 829.46m, 51.79m, 6.800m, 0m, new DateTime(2017,11,15)),
	new Loan("B Nelnet 2", 1339.27m, 67.44m, 6.550m, 0m, new DateTime(2017,11,18)),
	new Loan("VACU CC", 2960.54m, 102m, 10.990m, 0m, new DateTime(2017,11,18)),
	new Loan("JRAC HVAC", 6580m, 125m, 0m, 0m, new DateTime(2017,11,08)),
	new Loan("Durango", 24702.61m, 543.95m, 3.700m, 0m, new DateTime(2017,10,31)),
	new Loan("Discover Debt Consolidation", 29785.17m, 583.42m, 13.990m, 0m, new DateTime(2017,10,20)),
	new Loan("Mortgage", 183880.32m, 876.26m, 3.750m, 0m, new DateTime(2017,11,01)),

*/
#endregion Input Parameters

void Main()
{
	/*
	Snowball Algorythm
		1. Order by Amount Remaining 
			a. calc current amount remaining given:
				i. the current date
				ii. last payement date 
				iii. interest rate
				iv. projected payments since last payment date
					a. calc date
			b. Calculate any paidoff loans and
				
		2. Calculate Snowball projected dates given
			a. sum the payment amounts and extra payment amounts
			b. remove paid off loans from the calculations and mark as Paid Off
			c. Include in Snowball == true
			d. payment amount + extra amount - computed interest
			e. original payoff date
		
		3. Print Loans and Projected NUmber of months/payoff dates given the current information about eachloan and the snowball alg
	*/
	
	Loan previousLoan = null;
	//for each loan
	foreach (var loan in AllLoans.OrderBy(al => al.Original_AmountRemaining))
	{
		if(previousLoan != null)
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
								(loan.MonthlyPaymentDate.Year + (RunDate.Year - loan.MonthlyPaymentDate.Year)),
								monthNumber > 0 ? monthNumber : 12);
			for (int j = 0; j < interestDays; j++)
			{
				date = date.AddDays(1);
				computedAmountRemaining += computedAmountRemaining * (((loan.InterestRate / 12) / interestDays) / 100);
				if (date.Date <= RunDate.Date)
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
			
			if(previousLoan != null && loan.MonthsToPayOff == previousLoan.MonthsToPayOff)
				payment.Amount += previousLoan.PayoffMonthlyPaymentExtra;
			
			//when the amount left is smaller than the total payment we were going to make, reset
			if (computedAmountRemaining <= payment.Amount)
			{
				loan.PayoffMonthlyPaymentExtra = payment.Amount - computedAmountRemaining;
				payment.Amount = computedAmountRemaining;
			}
			computedAmountRemaining -= payment.Amount;
			loan.Payments.Add(payment);

			if (date.Date <= RunDate.Date)
			{
				loan.Current_AmountRemaining = computedAmountRemaining;
				loan.MonthsPaid++;
			}
			loan.MonthsToPayOff++;
		}
		loan.New_ProjectedPayoffDate = date;
		previousLoan = loan;
	}

	Console.Out.WriteLine($"Run Date: {RunDate}");
	Console.Out.WriteLine();
	foreach (var loan in AllLoans)
	{
		loan.ToString().Dump();
	}
}

// Define other methods and classes here
public class Loan
{
	public Loan(string name, decimal originalAmountRemaining, decimal monthlyPayment, decimal interestRate, decimal extraPaymentAmount, DateTime monthlyPaymentDate, bool includeInSnowball = true)
	{
		Name = name;
		Original_AmountRemaining = originalAmountRemaining;
		MonthlyPayment = monthlyPayment;
		InterestRate = interestRate;
		ExtraPaymentAmount = extraPaymentAmount;
		MonthlyPaymentDate = monthlyPaymentDate;
		IncludeInSnowball = includeInSnowball;
		
		Payments = new List<Payment>();
	}
	
	public string Name { get; set; }
	public bool IncludeInSnowball { get; set; }
	
	public decimal Original_AmountRemaining { get; set; }
	public decimal Current_AmountRemaining { get; set; }
	public decimal MonthlyPayment { get; set; }
	public decimal InterestRate { get; set; }
	public decimal ExtraPaymentAmount { get; set; }
	public DateTime MonthlyPaymentDate { get; set; }
	public decimal PayoffMonthlyPaymentExtra { get; set; }
	
	public DateTime Original_ProjectedPayOffDate { get; set; }
	public DateTime New_ProjectedPayoffDate { get; set; }
	public int MonthsToPayOff { get; set; }
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
		var yearsToPayoff = MonthsToPayOff > 0 ? MonthsToPayOff/12 > 1 ? MonthsToPayOff/12 : 0 : 0;
		if(yearsToPayoff > 0)
			sb.AppendLine($"Years to Payoff: {yearsToPayoff}");
			
		sb.AppendLine();	
		sb.AppendLine($"Months Paid to RunDate: {MonthsPaid}");

		sb.AppendLine();
		sb.AppendLine(new String('-', 150));
		sb.AppendLine($@"Payment Schedule");
		sb.AppendLine(new String('-', 150));
		foreach (var payment in Payments)
			sb.AppendLine($"Date {payment.Date.ToString("MM/dd/yyyy")} | Computed Loan Amount with Interest: {string.Format("{0:c}", payment.LoanAmount)} | Payment Amount: {string.Format("{0:c}", payment.Amount)} | Principal {string.Format("{0:c}", payment.Amount-payment.Interest)} | Interest {string.Format("{0:c}", payment.Interest)}");
		return sb.ToString();
	}
}

public class Payment
{
	public decimal Amount { get; set; }
	public decimal Interest { get; set; }
	public decimal LoanAmount { get; set; }
	public DateTime Date { get; set; }
	
}
