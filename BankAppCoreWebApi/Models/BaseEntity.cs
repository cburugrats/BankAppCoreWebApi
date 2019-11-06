using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi
{
	public class BaseEntity
	{
		public DateTime? createdDate { get; set; }
		public DateTime? updatedDate { get; set; }
	}
}
