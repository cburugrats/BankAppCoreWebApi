using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BankAppCoreWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAppCoreWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoneyTransfersController : ControllerBase
	{

		#region Havale Transfer
		[HttpPost]
		[Route("havale")]
		public int HavaleTranser([FromBody] MoneyTransferModel havaleModel)
		{

            if (havaleModel.receiverAccountNo!=havaleModel.senderAccountNo)
            {
                using (var db = new RugratsDbContext())
                {
                    Account senderAccount = db.Accounts.Where(x => x.accountNo == havaleModel.senderAccountNo).FirstOrDefault();//Gönderenin hesabı bulunuyor.

                    if (senderAccount.balance >= havaleModel.amount)//Eğer gönderen hesapta yeterli para yoksa.
                    {
                        Account receiverAccount = db.Accounts.Where(x => x.accountNo == havaleModel.receiverAccountNo && x.status == true).FirstOrDefault();//Alıcı hesap bulunuyor.
                        if (receiverAccount != null)//Alıcı hesap bulunduysa.
                        {
                            if (senderAccount.customerId==receiverAccount.customerId)
                            {
                                return 6;
                            }
                            senderAccount.balance -= havaleModel.amount;
                            senderAccount.netBalance -= havaleModel.amount;
                            receiverAccount.balance += havaleModel.amount;
                            receiverAccount.netBalance += havaleModel.amount;
                            try
                            {
                                MoneyTransfers moneyTransfers = new MoneyTransfers();
                                moneyTransfers.senderAccountNo = senderAccount.accountNo;
                                moneyTransfers.receiverAccountNo = receiverAccount.accountNo;
                                moneyTransfers.realizationTime = havaleModel.realizationTime;
                                moneyTransfers.statement = havaleModel.statement;
                                moneyTransfers.transferTypeId = 3;
                                moneyTransfers.balanceSent = havaleModel.amount;
                                moneyTransfers.status = true;
                                db.MoneyTransfers.Add(moneyTransfers);
                                db.SaveChanges();
                                return 1;//Para başarıyla diğer müşteriye aktarıldı.
                            }
                            catch (Exception)
                            {

                                return 4;//Veritabanına kaydedilirken hata oluştu!
                            }
                        }
                        else
                        {
                            return 3;//Alıcıya ait aktif hesap bulunamadı!
                        }

                    }
                    else
                    {
                        return 0;//Hesapta yeterli bakiye yok!
                    }
                }
            }
            else
            {
                return 5;
            }
		}

		#endregion Havale Transfer

		#region Virman Transfer

		[HttpPost]
		[Route("virman")]
		public int VirmanTranser([FromBody] MoneyTransferModel virmanModel)
		{
			using (var db = new RugratsDbContext())
			{
				Account senderAccount = db.Accounts.Where(x => x.accountNo == virmanModel.senderAccountNo).FirstOrDefault();
				Account receiverAccoount = db.Accounts.Where(x => x.accountNo == virmanModel.receiverAccountNo).FirstOrDefault();
				if (senderAccount.customerId != receiverAccoount.customerId)
				{
					return 2;//Para göndermeye çalıştğınız hesap size ait değil!
				}
				else
				{
					if (senderAccount.balance >= virmanModel.amount)
					{
						senderAccount.balance -= virmanModel.amount;
						senderAccount.netBalance -= virmanModel.amount;
						receiverAccoount.balance += virmanModel.amount;
						receiverAccoount.netBalance += virmanModel.amount;
						try
						{						
							var transferList = db.Database.ExecuteSqlCommand("exec [sp_Transfer] {0},{1},{2},{3},{4},{5}", virmanModel.senderAccountNo,virmanModel.receiverAccountNo,virmanModel.amount,4,virmanModel.realizationTime,virmanModel.statement);
							db.SaveChanges();
							return 1;//Para gönderme işlemi başarılı.
						}
						catch (Exception)
						{
							return 3;//Veritabanına kaydedilirken hata oluştu!
						}
					}
					else
						return 4;//Hesapta yeterli para yok.
				}
			}
		}
		#endregion Virman Transfer

		#region Get Transfer List

		[HttpGet("getTransferList/{accountNo}")]
		public IEnumerable<TransferListModel> GetTransfers(string accountNo)
		{
			using (var db = new RugratsDbContext())
			{

				/*var a = (from m in db.MoneyTransfers
						 join tt in db.TransferTypes on m.transferTypeId equals tt.Id
						 where m.senderAccountNo == accountNo || m.receiverAccountNo == accountNo
						 select new TransferListModel
						 {
                             amount = m.balanceSent,
							 receiverAccountNo = m.receiverAccountNo,
							 senderAccountNo = m.senderAccountNo,
							 createdDate = m.createdDate,
							 transferType=tt.transferType,
                             statement=m.statement
                             
						 }).ToList();*/
				var transferList = db.Query<TransferListModel>().FromSql("exec [sp_TransferListeleme] {0}", accountNo);
				return transferList.ToList();
			}
		}
		#endregion Get Transfer List
	}
}