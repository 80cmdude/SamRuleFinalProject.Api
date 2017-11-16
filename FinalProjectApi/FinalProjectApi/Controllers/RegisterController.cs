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
		public IActionResult Post([FromBody]User newUser, bool isUnitTest = false)
		{
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeCardNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] {$"{newUser.EmployeeCardNumber}"} );
				if (existingUser.Count > 0)
				{
					Response.Headers.Add("Error", $"The user {newUser.EmployeeCardNumber} already exists");
					return BadRequest();
				}
					
				User user = new User()
				{
					EmployeeCardNumber = newUser.EmployeeCardNumber,
					FirstName = newUser.FirstName,
					LastName = newUser.LastName,
					Password = newUser.Password,
					Email = newUser.Email,
					PhoneNumber = newUser.PhoneNumber,
				};
				user.Token = TokenHelper.GenerateToken(newUser.FirstName, newUser.LastName);
				var success = Program.UserDatabase.SaveItem(user);
				if (success == 1)
				{
					return Ok(user);
				}
				else
				{
					if (!isUnitTest)
					{
						Response.Headers.Add("Error", $"Failed adding user to the database");
					}
					return BadRequest();
				}
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
