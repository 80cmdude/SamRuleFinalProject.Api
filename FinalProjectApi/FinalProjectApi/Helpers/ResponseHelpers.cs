using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Helpers
{
    public static class ResponseHelpers
    {
		public static Dictionary<string,string> FailureResponse(Dictionary<string,string> response, string reason = "Something went wrong")
		{
			response.Add("success", "false");
			response.Add("reason", reason);

			return response;
		}

		public static Dictionary<string,string> TokenFailedResponse(Dictionary<string, string> response)
		{
			return FailureResponse(response, "Invalid Token");
		}
	}
}
