using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Helpers
{
    public static class ResponseHelpers
    {
		public static string FailureResponse(string reason = "Something went wrong")
		{
			object message = new
			{
				Success = false,
				Reason = reason,
			};
			string response = JsonConvert.SerializeObject(message);
			return response;
		}
	}
}
