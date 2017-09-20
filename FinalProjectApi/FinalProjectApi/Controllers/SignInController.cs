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
    public class SignInController : Controller
    {
		// POST: api/SignIn
		[Route("api/SignIn")]
		[HttpPost]
		public void Post([FromBody]User value)
		{
			string cake = "d";
		}

		[Route("api/SignOut")]
		[HttpPost]
		public string PostSignOut([FromBody]User user)
		{
			TokenHelper.ValidateToken(Request.Headers, user.ID);

			return "e";
		}
    }
}
