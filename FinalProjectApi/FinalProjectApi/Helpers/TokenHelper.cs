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
    }
}
