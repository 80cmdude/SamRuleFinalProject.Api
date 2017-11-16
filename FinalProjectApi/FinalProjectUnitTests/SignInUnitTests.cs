using FinalProjectApi.Controllers;
using FinalProjectApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProjectUnitTests
{
	[TestClass]
	public class SignInUnitTests
    {
		[TestMethod]
		public void SignInOk()
		{
			//Arrange
			SignInController controller = new SignInController();
			User user =  SetupHelper.RegisterUser();
			//Act

			var response = controller.Post(user, true) as OkObjectResult;

			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(200, response.StatusCode);
		}

		[TestMethod]
		public void SignInPasswordIncorrect()
		{
			//Arrange
			SignInController controller = new SignInController();
			User user = SetupHelper.RegisterUser();
			user.Password = "Test";
			//Act

			var response = controller.Post(user, true) as BadRequestResult;

			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(400, response.StatusCode);
		}

		[TestMethod]
		public void SignInUserDoesNotExist()
		{
			//Arrange
			SignInController controller = new SignInController();
			User user = new User();
			user.EmployeeCardNumber = -1;
			//Act

			var response = controller.Post(user, true) as BadRequestResult;

			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(400, response.StatusCode);
		}

		[TestMethod]
		public void SignInInvalidData()
		{
			//Arrange
			SignInController controller = new SignInController();
			User user = new User();
			user.EmployeeCardNumber = -500;
			//Act

			var response = controller.Post(user, true) as BadRequestResult;

			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(400, response.StatusCode);
		}
	}
}
