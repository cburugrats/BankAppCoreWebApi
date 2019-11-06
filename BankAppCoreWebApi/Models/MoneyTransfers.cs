using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class MoneyTransfers:BaseEntity
	{
		public int Id { get; set; }
		public string senderAccountNo { get; set; }
		public decimal balanceSent { get; set; }
		public string receiverAccountNo { get; set; }
		public DateTime realizationTime { get; set; }
		public bool status { get; set; }
		public int transferTypeId { get; set; }
		public string statement { get; set; }
	}
}