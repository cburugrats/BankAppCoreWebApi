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
		// POST api/login
		[HttpPut("updateUser")]
		public async Task<int> PutUserAsync([FromBody] UpdateUserModel updateModel)
		{
			using (var db = new RugratsDbContext())
			{
				if (!(updateModel.customerId>0))
				{
					return 0;//Geçersiz customerId girdiniz!
				}
				User user = await db.Users.FirstOrDefaultAsync(x => x.customerId == updateModel.customerId);
				Customer customer = await db.Customers.FirstOrDefaultAsync(x => x.Id == updateModel.customerId);
				try
				{
					var transfer = db.Database.ExecuteSqlCommand("exec [sp_Bilgi] {0},{1},{2},{3},{4},{5},{6},{7}", updateModel.customerId, updateModel.userName, updateModel.userPassword, updateModel.firstname, updateModel.surname, updateModel.dateOfBirth, updateModel.phoneNumber, updateModel.eMail);
					return 1;
				}
				catch (Exception)
				{
					return 2;//Veritabanına kaydedilirken hata oluştu!
				}							}
		}
		#endregion
	}
}