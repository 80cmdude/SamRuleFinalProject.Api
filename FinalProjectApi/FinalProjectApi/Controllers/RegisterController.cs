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
    [Route("api/Register")]
    public class RegisterController : Controller
    {
		// POST: api/Register
		[HttpPost]
		public IActionResult Post([FromBody]User newUser)
		{
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] {$"{newUser.EmployeeNumber}"} );
				if (existingUser.Count > 0)
				{
					Response.Headers.Add("Error", $"The user {newUser.EmployeeNumber} already exists");
					return BadRequest();
				}
					

				User user = new User()
				{
					EmployeeNumber = newUser.EmployeeNumber,
					FirstName = newUser.FirstName,
					LastName = newUser.LastName,
					Password = newUser.Password,
				};
				user.Token = TokenHelper.GenerateToken(newUser.FirstName, newUser.LastName);
				var success = Program.UserDatabase.SaveItem(user);
				if (success == 1)
				{
					return Ok(user);
				}
				else
				{
					Response.Headers.Add("Error", $"Failed adding user to the database");
					return BadRequest();
				}
			}
			catch (Exception e)
			{
				Response.Headers.Add("Error", $"Information sent was not correct");
				return BadRequest();
			}
		}
	}
}
