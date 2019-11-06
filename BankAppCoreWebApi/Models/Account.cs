using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class Account : BaseEntity
	{
		[Key]
		public string accountNo { get; set; }
		public int customerId { get; set; }
		public decimal balance { get; set; }
		public decimal blockageAmount { get; set; }
		public bool status { get; set; }
		public decimal netBalance { get; set; }
		public DateTime? openingDate { get; set; }
		public DateTime? lastTransactionDate { get; set; }
	}

}
