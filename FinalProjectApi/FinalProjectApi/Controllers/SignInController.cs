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
		public Dictionary<string,string> Post([FromBody]User user)
		{
			object message;
			Dictionary<string, string> response = new Dictionary<string, string>();
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] { $"{user.EmployeeNumber}" });

				if (existingUser.Count == 0)
					return ResponseHelpers.FailureResponse(response, "User does not exist");

				if (existingUser[0].Password == user.Password)
				{
					existingUser[0].Token = TokenHelper.GenerateToken(user.FirstName, user.LastName);

					var success = Program.UserDatabase.SaveItem(existingUser[0]);
					if (success != 0)
					{
						response.Add("success", "true");
						response.Add("id", $"{existingUser[0].ID}");
						response.Add("token", $"{existingUser[0].Token}");
						return response;
					}
					else
					{
						return ResponseHelpers.FailureResponse(response, "Failed adding user to database");
					}
				}
				return ResponseHelpers.FailureResponse(response, "Invalid Password");

			}
			catch (Exception e)
			{
				return ResponseHelpers.FailureResponse(response, "Information sent was not correct");
			}
		}

		[Route("api/SignOut")]
		[HttpPost]
		public Dictionary<string,string> PostSignOut([FromBody]User user)
		{
			Dictionary<string, string> response = new Dictionary<string, string>();
			if (!TokenHelper.ValidateToken(Request.Headers, user.ID))
				return ResponseHelpers.TokenFailedResponse(response);

			return response;
		}
    }
}
