using System;
using System.Collections.Generic;
using System.Text;
using FinalProjectApi.Models;
using FinalProjectApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectUnitTests
{
    public static class SetupHelper
    {
		public static User CreateNewUser()
		{
			Random rnd = new Random();
			int employeeCardNumber = rnd.Next(1, 4000);

			User user = new User()
			{
				EmployeeCardNumber = employeeCardNumber,
				FirstName = "Unit",
				LastName = "Test",
				Password = "Password",
				Email = "Test@test.com",
				PhoneNumber = "123456789"
			};

			return user;
		}

		public static User RegisterUser()
		{
			User user = CreateNewUser();
			RegisterController controller = new RegisterController();
			var response = controller.Post(user, true) as OkObjectResult;
			if (response != null)
			{
				return response.Value as User;
			}
			return new User();
		}
    }
}
