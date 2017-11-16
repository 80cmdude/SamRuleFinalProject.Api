using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinalProjectApi.Controllers;
using FinalProjectApi.Models;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectUnitTests
{
    [TestClass]
    public class RegisterUnitTests
    {
        [TestMethod]
        public void RegisterUserOk()
        {
			//Arrange
			User user = SetupHelper.CreateNewUser();
			User responseUser = new User();

			RegisterController controller = new RegisterController();
			
			//Act
			var response = controller.Post(user, true) as OkObjectResult;
			if (response != null)
			{
				responseUser = response.Value as User;
			}
			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(user.EmployeeCardNumber, responseUser.EmployeeCardNumber);
        }

		[TestMethod]
		public void RegisterUserInvalidData()
		{
			//Arrange
			User user = new User();
			User responseUser = new User();
			user.ID = 1;
			RegisterController controller = new RegisterController();

			//Act
			var response = controller.Post(user, true) as BadRequestResult;

			//Assert
			Assert.IsNotNull(response);
			Assert.AreEqual(400 ,response.StatusCode);
		}

		[TestMethod]
		public void RegisterUserExists()
		{
			//Arrange
			User user = SetupHelper.CreateNewUser();
			User responseUser = new User();
			RegisterController controller = new RegisterController();

			//Act
			var response = controller.Post(user, true) as OkObjectResult;
			if (response != null)
			{
				responseUser = response.Value as User;
			}
			var responseSecond = controller.Post(responseUser, true) as BadRequestResult;

			//Assert
			Assert.IsNotNull(responseSecond);
			Assert.AreEqual(400, responseSecond.StatusCode);
		}
    }
}
