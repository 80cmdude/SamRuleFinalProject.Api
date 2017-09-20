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
		private static Database _UserDatabase;

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
				if (_UserDatabase == null)
				{
					_UserDatabase = new Database("Users");
					_UserDatabase.CreateTable<User>();
					return _UserDatabase;
				}
				return _UserDatabase;
			}
		}
	}
}
