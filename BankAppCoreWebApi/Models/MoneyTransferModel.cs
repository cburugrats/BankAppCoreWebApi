using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class MoneyTransferModel
	{
		[Key]
		public string receiverAccountNo { get; set; }
		[Key]
		public string senderAccountNo { get; set; }
		public decimal amount { get; set; }
		public DateTime realizationTime { get; set; }
		public string statement { get; set; }
	}
}
