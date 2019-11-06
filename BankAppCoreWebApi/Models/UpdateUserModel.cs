﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppCoreWebApi.Models
{
	public class UpdateUserModel
	{
		public int customerId { get; set; }
		public string userName { get; set; }
		public string userPassword { get; set; }
		public string firstname { get; set; }
		public string surname { get; set; }
		public DateTime dateOfBirth { get; set; }
		public long phoneNumber { get; set; }
		public string eMail { get; set; }
	}
}
