using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BankAppCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BankAppCoreWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HgsController : ControllerBase
	{
		#region Register Hgs
		// POST api/hgs
		[HttpPost]
		public async Task<int> PostRegisterAsync([FromBody] HgsRegisterModel registerModel)
		{
			using (var db =new RugratsDbContext())
			{
				Account account	=  db.Accounts.Where(x => x.accountNo == registerModel.accountNo).FirstOrDefault();
				if (account != null)
				{
					if (account.netBalance >= registerModel.balance)
					{
						string url = "https://rugratshgs.azurewebsites.net/api/user";
						HgsUserModel userModel=new HgsUserModel{ balance=registerModel.balance};
						int response= await HttpRequestAsync(userModel, "post", url);
						if (response>=1000)
						{
							account.balance -= registerModel.balance;
							account.netBalance -= registerModel.balance;					
							var transferList = db.Database.ExecuteSqlCommand("exec [sp_Transfer] {0},{1},{2},{3},{4},{5}", registerModel.accountNo, registerModel.accountNo, registerModel.balance, 5, DateTime.Now, "Hgs'ye Para Yatırıldı");
							db.SaveChanges();
						}
						return response;
						//Eğer dönen sayı 1000'den küçükse hata oluşmuştur.
						//Eğer 1000 veya 1000'den büyükse kayıt başarılı olmuştur ve yeni HgsNo dönmüştür.
					}
					else
						return 5;//Hesapta yeterli bakiye yok!
				}
				else
					return 6;//Hesap bulunamadı!
			}		
		}
		#endregion Register Hgs

		#region Get Hgs User By HgsNo
		// GET api/hgs/5
		[HttpGet("{HgsNo}")]
		public async Task<HgsUserModel> GetByIdAsync(int HgsNo)
		{
			if (HgsNo==null)
			{
				return null;
			}
			string url = "https://rugratshgs.azurewebsites.net/api/user/" + HgsNo;
			HgsUserModel result = new HgsUserModel();
			using (var client = new HttpClient())
			{
				var task = client.GetAsync(url)
				  .ContinueWith((taskwithresponse) =>
				  {
					  var response = taskwithresponse.Result;
					  var jsonString = response.Content.ReadAsStringAsync();
					  jsonString.Wait();
					  result = JsonConvert.DeserializeObject<HgsUserModel>(jsonString.Result);

				  });
				task.Wait();
			}
			return result;
		}

		#endregion Get Hgs User By HgsNo

		#region To Deposit Money Hgs
		// PUT api/user/toDepositMoney
		[HttpPut("toDepositMoney")]
		public async Task<int> PutToDepositMoneyAsync([FromBody] HgsRegisterModel registerModel)
		{
			using (var db = new RugratsDbContext())
			{
				Account account = db.Accounts.Where(x => x.accountNo == registerModel.accountNo).FirstOrDefault();
				if (account != null)
				{
					if (account.netBalance >= registerModel.balance)
					{
						string url = "https://rugratshgs.azurewebsites.net/api/user/toDepositMoney";
						HgsUserModel userModel = new HgsUserModel { balance = registerModel.balance,HgsNo=registerModel.HgsNo };
						int returnValue = await HttpRequestAsync(userModel, "put", url);
						if (returnValue == 1)
						{
							account.balance -= registerModel.balance;
							account.netBalance -= registerModel.balance;
							var transfer = db.Database.ExecuteSqlCommand("exec [sp_Transfer] {0},{1},{2},{3},{4},{5}", registerModel.accountNo, registerModel.accountNo, registerModel.balance, 5, DateTime.Now, "Hgs'ye Para Yatırıldı");
							db.SaveChanges();
						}
						return returnValue;
					}
					else
						return 5;//Hesapta yeterli bakiye yok!
				}
				else
					return 6;//Hesap bulunamadı!
			}
		}

		#endregion To Deposit Money Hgs

		//#region With Draw Money Hgs
		//// PUT api/user/withDrawMoney
		//[HttpPut("withDrawMoney")]
		//public async Task<int> PutWithDrawMoneyAsync(int id, [FromBody] HgsUserModel userModel)
		//{
		//	string url = "https://bankapphgswebapi.azurewebsites.net/api/user/withDrawMoney";
		//	return await HttpRequestAsync(userModel, "put", url);
		//}
		//#endregion With Draw Money Hgs

		#region Credit
		[HttpPost]
		[Route("getCredit")]
		public async Task<int> WithDrawMoneyAsync([FromBody] CreditModel credit)
		{
			string response = "";
			string url = "https://ml-python.herokuapp.com/api";
			using (var client = new HttpClient())
			{
				var result = new HttpResponseMessage();
				client.BaseAddress = new Uri(url);
				var content = new StringContent(JsonConvert.SerializeObject(credit), Encoding.UTF8, "application/json");
				result = await client.PostAsync(url, content);
				if (result.IsSuccessStatusCode)
				{
					result.EnsureSuccessStatusCode();
					response = await result.Content.ReadAsStringAsync();
				}
			}

			return Convert.ToInt32(response);
		}
		#endregion

		#region Http Request 
		public async Task<int> HttpRequestAsync([FromBody] HgsUserModel userModel, string requestType, string url)
		{
			string response = "";
			using (var client = new HttpClient())
			{
				var result = new HttpResponseMessage();
				client.BaseAddress = new Uri(url);
				var content = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
				if (requestType.Equals("put"))
				{
					result = await client.PutAsync(url, content);
				}
				else if (requestType.Equals("post"))
				{
					result = await client.PostAsync(url, content);
				}
				else
				{
					result = await client.PostAsync(url, content);
				}
				if (result.IsSuccessStatusCode)
				{
					result.EnsureSuccessStatusCode();
					response = await result.Content.ReadAsStringAsync();
				}
			}
			return Convert.ToInt32(response);
		}
		#endregion Http Request 
	}
}