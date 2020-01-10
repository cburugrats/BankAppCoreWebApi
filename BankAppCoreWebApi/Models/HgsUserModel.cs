using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class HgsUserModel
	{
		[Key]
		public int Id { get; set; }
		public long TcNo { get; set; }
		public int HgsNo { get; set; }
		public decimal balance { get; set; }
	}
}
