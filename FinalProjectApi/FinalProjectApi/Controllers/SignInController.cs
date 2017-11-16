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
		public IActionResult Post([FromBody]User user, bool isUnitTest = false)
		{
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeCardNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] { $"{user.EmployeeCardNumber}" });

				if (existingUser.Count == 0)
				{
					if (!isUnitTest)
					{
						Response.Headers.Add("Error", $"User does not exist");
					}
					return BadRequest();
				}

				if (existingUser[0].Password == user.Password)
				{
					existingUser[0].Token = TokenHelper.GenerateToken(user.FirstName, user.LastName);

					var success = Program.UserDatabase.SaveItem(existingUser[0]);
					
					return Ok(existingUser[0]);
				}
				if (!isUnitTest)
				{
					Response.Headers.Add("Error", $"Invalid Password");
				}
				return BadRequest();

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
