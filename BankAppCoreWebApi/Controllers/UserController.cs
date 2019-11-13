using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAppCoreWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		#region getUsers
		// GET api/getUsers
		[HttpGet]
		[Route("getUsers")]
		public IEnumerable<User> Get()
		{
			using (var db = new RugratsDbContext())
			{
				var temp = db.Users.ToList();
				return temp;
			}
		}
		#endregion

		#region GetUserById
		// GET api/user/5
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetAsync(int id)
		{
			var db = new RugratsDbContext();
			var tempUser = await db.Users.FindAsync(id);

			if (tempUser == null)
			{
				return NotFound();
			}
			return tempUser;
		}
		#endregion GetUserById

		#region Update User and Customer Informations
		// PUT api/hgs/updateUser
		[HttpPut("updateUser")]
		public async Task<int> PutUserAsync([FromBody] UpdateUserModel updateModel)
		{
			using (var db = new RugratsDbContext())
			{
				User user = await db.Users.FirstOrDefaultAsync(x => x.TcIdentityKey == updateModel.TcIdentityKey);
				if (user==null)
				{
					return 3;//Bu TC'ye kayıtlı kullanıcı bulunamadı!
				}			
				Customer customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == user.customerId);
				try
				{
					var transfer = db.Database.ExecuteSqlCommand("exec [sp_Bilgi] {0},{1},{2},{3},{4},{5},{6},{7}", user.customerId, updateModel.userName, updateModel.userPassword, updateModel.firstname, updateModel.surname, updateModel.dateOfBirth, updateModel.phoneNumber, updateModel.eMail);
					return 1;
				}
				catch (Exception)
				{
					return 2;//Veritabanına kaydedilirken hata oluştu!
				}
			}
		}
		#endregion

		#region Get Update User By TcIdentityKey
		// GET api/user/
		[HttpGet("getUpadateUser/{TcIdentityKey}")]
		public async Task<ActionResult<UpdateUserModel>> GetAsync(long TcIdentityKey)
		{
			using (var db = new RugratsDbContext())
			{
				var tempUser = await db.Users.Where(x=> x.TcIdentityKey == TcIdentityKey).FirstOrDefaultAsync();

				if (tempUser == null)
				{
					return null;
				}
				else
				{
					var tempCustomer = await db.Customers.Where(x => x.Id == tempUser.customerId).FirstOrDefaultAsync();
					if (tempCustomer != null)
					{
						UpdateUserModel updateUserModel = new UpdateUserModel { userName = tempUser.userName, userPassword = tempUser.userPassword, TcIdentityKey = tempUser.TcIdentityKey, firstname = tempCustomer.firstname, surname = tempCustomer.surname, eMail = tempCustomer.eMail, dateOfBirth = tempCustomer.dateOfBirth, phoneNumber = tempCustomer.phoneNumber };
						return updateUserModel;
					}
					else
						return null; 
				}
				
			}
		}
		#endregion
	}
}