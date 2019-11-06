using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi
{
	public class User:BaseEntity
	{
		public int Id { get; set; }
		public long TcIdentityKey { get; set; }
		public int customerId { get; set; }
		public string userName { get; set; }
		public string userPassword { get; set; }
	}
}