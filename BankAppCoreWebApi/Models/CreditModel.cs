using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class CreditModel
	{
		public int krediMiktari { get; set; }
		public int yas { get; set; }
		public int evDurumu { get; set; }
		public int aldigi_kredi_sayi { get; set; }
		public int telefonDurumu { get; set; }
	}
}
