using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankAppCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
		#region Login
		// POST api/login
		[HttpPost]
		public int PostLogin([FromBody] User user)
		{
			var db = new RugratsDbContext();
			var isUserValid = db.Users.FirstOrDefault(x => x.TcIdentityKey == user.TcIdentityKey && x.userPassword == user.userPassword);

			if (isUserValid != null)
			{
				return 1;
			}
			return 0;
		}
		#endregion
	}
}