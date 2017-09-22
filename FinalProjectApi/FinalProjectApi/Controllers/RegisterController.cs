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
		public Dictionary<string,string> Post([FromBody]User newUser)
		{
			Dictionary<string,string> response = new Dictionary<string, string>();
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] {$"{newUser.EmployeeNumber}"} );
				if (existingUser.Count > 0)
					return ResponseHelpers.FailureResponse(response, "User already exists");

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
					response.Add("success", "true");
					response.Add("token", $"{user.Token}");
					response.Add("id", $"{user.ID}");
					return response;
				}
				else
				{
					return ResponseHelpers.FailureResponse(response, "Failed adding user to database");
				}
			}
			catch (Exception e)
			{
				return ResponseHelpers.FailureResponse(response, "Information sent was not correct");
			}
		}
	}
}
