using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAppCoreWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
		#region Register
		// POST api/register
		[HttpPost]
		public int PostRegister([FromBody] Register registerModel)
		{
			var db = new RugratsDbContext();
			var tempCustomer = db.Customers.FirstOrDefault(x => x.eMail == registerModel.eMail);
			var tempUser = db.Users.FirstOrDefault(x => x.TcIdentityKey == registerModel.TcIdentityKey);
			if (tempCustomer != null)
			{
				return 2;// Aynı emailde daha önce kayıt olmuş müşteri var!
			}
			else if(tempUser!=null)
			{
				return 3;// Aynı TCno'da daha önce kayıt olmuş kullanıcı var!
			}
			else
			{
				Customer customer = new Customer();
				customer.firstname = registerModel.firstname;
				customer.surname = registerModel.surname;
				if (DateTime.Now.Year-registerModel.dateOfBirth.Year<18)
				{
					return 4;//18 yaşından küçükler kayıt olamaz!
				}
				customer.dateOfBirth = registerModel.dateOfBirth;
				customer.phoneNumber = registerModel.phoneNumber;
				customer.eMail = registerModel.eMail;
				try
				{
					db.Customers.Add(customer);
					db.SaveChanges();
					int customerId = customer.Id;
					User user = new User();
					user.TcIdentityKey = registerModel.TcIdentityKey;
					user.updatedDate = registerModel.updatedDate;
					user.createdDate = registerModel.createdDate;
					user.customerId = customerId;
					user.userName = registerModel.userName;
					user.userPassword = registerModel.userPassword;
					user.createdDate = DateTime.Now;
					user.updatedDate = DateTime.Now;
					db.Users.Add(user);
					db.SaveChanges();
				}

				catch (System.Exception)
				{
					return 0;
				}
			}
			return 1;
		}
		#endregion
	}
}