﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectApi.Models;
using FinalProjectApi.Helpers;
using Microsoft.Extensions.Primitives;

namespace FinalProjectApi.Controllers
{
	[Produces("application/json")]
	public class CurrencyController : Controller
	{
		// POST: api/Currency
		[Route("/api/Transaction")]
		[HttpPost]
		public IActionResult PostTransaction([FromBody]Transaction value, bool isUnitTest = false)
		{
			try
			{
				if (!TokenHelper.ValidateToken(Request.Headers))
				{
					Response.Headers.Add("Error", "Invalid Token");
					return BadRequest();
				}
				if (!Request.Headers.TryGetValue("Id", out var id))
				{
					Response.Headers.Add("Error", "Invalid User Id");
					return BadRequest();
				}


				//HERE I WILL SEND OFF THE CREDIT INFROMATION TO BE DEALT WITH BY MY BACK END TEAM.

				string queryGetCurrentPoints = "SELECT * FROM USER WHERE ID = ?";

				var User = Program.UserDatabase.Query<User>(queryGetCurrentPoints, new string[] { $"{id}" });
				decimal currentBalance = User[0].Balance;


				value.Date = DateTime.Now;
				value.PreviosBalance = currentBalance;
				value.AddedAmount = value.ProductPrice;
				value.NewBalance = currentBalance + value.AddedAmount;

				if (value.NewBalance <= 0)
				{
					if (!isUnitTest)
					{
						Response.Headers.Add("Error", "Insufficent credit to make payment.");
					}
					return BadRequest();
				}

				User[0].Balance = value.NewBalance;

				int transactionID = Program.TransactionsDatabase.SaveItem<Transaction>(value);
				Program.UserDatabase.SaveItem<User>(User[0]);

				return Ok(User[0]);
			}
			catch (Exception e)
			{
				if (!isUnitTest)
				{
					Response.Headers.Add("Error", $"Information sent was not correct");
				}
				return BadRequest();
			}
		}

		[Route("/api/transactions")]
		[HttpGet]
		public IActionResult GetTransactions(bool isUnitTest = false)
		{
			try
			{
				if (!TokenHelper.ValidateToken(Request.Headers))
				{
					Response.Headers.Add("Error", "Invalid Token");
					return BadRequest();
				}
				if (!Request.Headers.TryGetValue("Id", out var id))
				{
					Response.Headers.Add("Error", "Invalid User Id");
					return BadRequest();
				}

				var transactions = Program.TransactionsDatabase.GetItems<Transaction>();

				IEnumerable<Transaction> userTransactions = transactions.Where(c => c.UserId == Convert.ToInt32(id)).ToList<Transaction>();

				return Ok(userTransactions);
			}
			catch (Exception e)
			{
				if (!isUnitTest)
				{
					Response.Headers.Add("Error", $"Information sent was not correct");
				}
				return BadRequest();
			}
		}

		[Route("/api/Balance")]
		[HttpGet]
		public IActionResult GetBalance(bool isUnitTest = false)
		{
			try
			{
				if (!TokenHelper.ValidateToken(Request.Headers))
				{
					Response.Headers.Add("Error", "Invalid Token");
					return BadRequest();
				}
				if (!Request.Headers.TryGetValue("Id", out var id))
				{
					Response.Headers.Add("Error", "Invalid User Id");
					return BadRequest();
				}

				string queryGetCurrentPoints = "SELECT * FROM USER WHERE ID = ?";

				var User = Program.UserDatabase.Query<User>(queryGetCurrentPoints, new string[] { $"{id}" });
				decimal currentBalance = User[0].Balance;

				return Ok(currentBalance);
			}
			catch (Exception e)
			{
				if (!isUnitTest)
				{
					Response.Headers.Add("Error", $"Information sent was not correct");
				}
				return BadRequest();
			}
		}
	}
}
