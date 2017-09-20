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
		public string Post([FromBody]User user)
		{
			object message;
			string response;
			string queryCheckUser = "SELECT * FROM USER WHERE EmployeeNumber = ?";

			try
			{
				var existingUser = Program.UserDatabase.Query<User>(queryCheckUser, new string[] { $"{user.EmployeeNumber}" });

				if (existingUser.Count == 0)
					return ResponseHelpers.FailureResponse("User does not exist");

				if (existingUser[0].Password == user.Password)
				{
					existingUser[0].Token = TokenHelper.GenerateToken(user.FirstName, user.LastName);

					var success = Program.UserDatabase.SaveItem(existingUser[0]);
					if (success != 0)
					{
						message = new
						{
							success = true,
							Id = existingUser[0].ID,
							Token = existingUser[0].Token,
						};
						response = JsonConvert.SerializeObject(message);
						return response;
					}
					else
					{
						return ResponseHelpers.FailureResponse("Failed adding user to database");
					}
				}
				return "Invalid Password";
				
			}
			catch (Exception e)
			{
				return ResponseHelpers.FailureResponse("Information sent was not correct");
			}
		}

		[Route("api/SignOut")]
		[HttpPost]
		public string PostSignOut([FromBody]User user)
		{
			if (!TokenHelper.ValidateToken(Request.Headers, user.ID))
				return ResponseHelpers.TokenFailedResponse();

			return "e";
		}
    }
}
