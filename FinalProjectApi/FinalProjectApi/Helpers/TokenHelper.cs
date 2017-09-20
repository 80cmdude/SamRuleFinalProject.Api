using FinalProjectApi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Helpers
{
    public static class TokenHelper
    {
		public static string GenerateToken(string firstName, string lastName)
		{
			string token = firstName + lastName + DateTime.Now.ToString();
			return token;
		}

		public static bool ValidateToken(IHeaderDictionary headers, int id)
		{
			string FullToken;
			if (!headers.TryGetValue("Authorization", out var authToken))
				return false;

			FullToken = authToken.ToString();
			var token = FullToken.Substring(6);

			List<User> users = Program.UserDatabase.Query<User>("SELECT * FROM USER WHERE ID = ?", new string[] {$"{id}"});

			if (users[0].Token != token)
			{
				return false;
			}

			return true;
			
		}
    }
}
