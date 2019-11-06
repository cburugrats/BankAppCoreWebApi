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
    public class CustomerController : ControllerBase
    {
		// GET api/user
		[HttpGet]
		[Route("getCustomers")]
		public IEnumerable<Customer> Get()
		{
			using (var db = new RugratsDbContext())
			{
				var temp = db.Customers.ToList();
				return temp;
			}
		}

		// GET api/{controller}/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Customer>> GetAsync(int id)
		{
			var db = new RugratsDbContext();
			var tempCustomer = await db.Customers.FindAsync(id);

			if (tempCustomer == null)
			{
				return NotFound();
			}
			return tempCustomer;
		}

		[HttpPost]
		[Route("registerCustomer")]
		public int PostRegister([FromBody] Customer customer)
		{
			var db = new RugratsDbContext();
			try
			{
				db.Customers.Add(customer);
				db.SaveChanges();
			}
			catch (Exception)
			{
				return 0;
			}
			return 1;
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{

		}
	}


}