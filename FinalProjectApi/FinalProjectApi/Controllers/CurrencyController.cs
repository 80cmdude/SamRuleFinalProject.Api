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
			if (TokenHelper.ValidateToken(Request.Headers))
				return BadRequest();
			if (!Request.Headers.TryGetValue("Id", out var id))
				return BadRequest();
			string queryGetCurrentPoints = "SELECT * FROM USER WHERE ID = ?";

			var User = Program.UserDatabase.Query<User>(queryGetCurrentPoints, new string[] { $"{id}" });
			decimal currentBalance = User[0].Balance;

			value.Date = DateTime.Now;
			value.PreviosBalance = currentBalance;
			value.DetuctedAmount = value.ProductPrice;
			value.NewBalance = currentBalance - value.DetuctedAmount;
			User[0].Balance = value.NewBalance;

			int transactionID = Program.TransactionsDatabase.SaveItem<Transaction>(value);
			Program.UserDatabase.SaveItem<User>(User[0]);

			return Ok(User[0]);
        }
    }
}
