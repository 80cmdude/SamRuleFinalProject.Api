using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using FinalProjectApi.Data;
using FinalProjectApi.Models;

namespace FinalProjectApi
{
    public class Program
    {
		private static Database _orderDatabase;
		private static Database _userDatabase;
		private static Database _transactionDatabase;

		public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }

		public static Database OrderDatabase
		{
			get
			{
				if (_orderDatabase == null)
				{
					_orderDatabase = new Database("Orders");
					_orderDatabase.CreateTable<Order>();
					return _orderDatabase;
				}
				return _orderDatabase;
			}
		}

		public static Database UserDatabase
		{
			get
			{
				if (_userDatabase == null)
				{
					_userDatabase = new Database("Users");
					_userDatabase.CreateTable<User>();
					return _userDatabase;
				}
				return _userDatabase;
			}
		}

		public static Database TransactionsDatabase
		{
			get
			{
				if (_transactionDatabase == null)
				{
					_transactionDatabase = new Database("Transactions");
					_transactionDatabase.CreateTable<Transaction>();
					return _transactionDatabase;
				}
				return _transactionDatabase;
			}
		}
	}
}
