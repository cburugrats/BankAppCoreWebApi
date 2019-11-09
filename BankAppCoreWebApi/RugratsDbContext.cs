using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAppCoreWebApi
{
	public class RugratsDbContext:DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Customer> Customers{ get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<MoneyTransfers> MoneyTransfers { get; set; }
		public DbSet<TransferTypesModel> TransferTypes { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(@"Server=tcp:rugrats.database.windows.net,1433;Initial Catalog=rugratsdb;Persist Security Info=False;User ID=rugrat;Password=Pas314159;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Query<TransferListModel>();
		}
	}
}
