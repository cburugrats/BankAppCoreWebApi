using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class AccountNoAndMoney
	{
		public string accountNo { get; set; }
		public decimal Balance { get; set; }
	}
}
