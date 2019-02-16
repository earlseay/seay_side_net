using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaySideNet.Utilities
{
	public static class ExtensionMethods
	{
		public static bool IsBeforeNextPaymentDate(this DateTime dateToCheck, DateTime lastpaymentDate, int daysUntilNextPayment)
		{
			return dateToCheck.Date <= lastpaymentDate.Date && dateToCheck.Date >= lastpaymentDate.AddDays(daysUntilNextPayment).Date;
		}
	}
}