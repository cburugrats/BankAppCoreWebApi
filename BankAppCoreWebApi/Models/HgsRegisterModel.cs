using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class HgsRegisterModel
	{
		public string accountNo { get; set; }
		public int HgsNo { get; set; }
		public decimal balance { get; set; }
	}
}
