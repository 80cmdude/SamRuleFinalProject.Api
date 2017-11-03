using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProjectApi.Models;
using FinalProjectApi.Helpers;
using Newtonsoft.Json;

namespace FinalProjectApi.Controllers
{
    [Produces("application/json")]
    public class SignInController : Controller
    {
		// POST: api/SignIn
		[Route("api/SignIn")]
		[HttpPost]
		public IActionResult Post([FromBody]User user)
		{
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] { $"{user.EmployeeNumber}" });

				if (existingUser.Count == 0)
				{
					Response.Headers.Add("Error", $"User does not exist");
					return BadRequest();
				}

				if (existingUser[0].Password == user.Password)
				{
					existingUser[0].Token = TokenHelper.GenerateToken(user.FirstName, user.LastName);

					var success = Program.UserDatabase.SaveItem(existingUser[0]);

					User currentUser = new User()
					{
						FirstName = user.FirstName,
						LastName = user.LastName,
						EmployeeNumber = user.EmployeeNumber,
						ID = existingUser[0].ID,
						Token = existingUser[0].Token,
					};
					return Ok(currentUser);
				}
				Response.Headers.Add("Error", $"Invalid Password");
				return BadRequest();

			}
			catch (Exception e)
			{
				Response.Headers.Add("Error", $"Information sent was not correct");
				return BadRequest();
			}
		}
    }
}
