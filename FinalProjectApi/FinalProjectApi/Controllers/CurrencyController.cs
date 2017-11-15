using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectApi.Models;
using FinalProjectApi.Helpers;

namespace FinalProjectApi.Controllers
{
	[Produces("application/json")]
	public class CurrencyController : Controller
	{
		// POST: api/Currency
		[Route("/api/Transaction")]
		[HttpPost]
		public IActionResult PostTransaction([FromBody]Transaction value)
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
				Response.Headers.Add("Error", "Insufficent credit to make payment.");
				return BadRequest();
			}

			User[0].Balance = value.NewBalance;

			int transactionID = Program.TransactionsDatabase.SaveItem<Transaction>(value);
			Program.UserDatabase.SaveItem<User>(User[0]);

			return Ok(User[0]);
		}

		[Route("/api/transactions")]
		[HttpGet]
		public IActionResult GetTransactions()
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

		[Route("/api/Balance")]
		[HttpGet]
		public IActionResult GetBalance()
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
    }
}
