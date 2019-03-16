using Moq;
using NUnit.Framework;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using ProjectManagerAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerAPI.Tests.UnitTests
{
    [TestFixture]
    public class UserControllerTest
    {
        Mock<IUserBusiness> mock = new Mock<IUserBusiness>();


        [Test]
        public void Get_All_Users()
        {
            // Arrange

            mock.Setup(a => a.GetAllUsers()).Returns(new List<UserDTO> { new UserDTO { User_Id = 1, FirstName = "Fname", LastName = "LName", Employee_Id = 1 } });
            UserController controller = new UserController(mock.Object);

            // Act
            IEnumerable<UserDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Fname", result.ElementAt(0).FirstName);
        }

       

        [Test]
        public void Get_User_By_Id()
        {
            // Arrange
            mock.Setup(a => a.GetUserByUserId(1)).Returns( new UserDTO { User_Id = 1, FirstName = "Fname", LastName = "LName", Employee_Id = 1 } );
            UserController controller = new UserController(mock.Object);

            // Act
            UserDTO result = controller.Get(1);

            // Assert
            Assert.AreEqual("Fname", result.FirstName);
        }

        [Test]
        public void Create_a_user()
        {
            // Arrange
            mock.Setup(a => a.CreateUser(It.IsAny<UserDTO>())).Returns(true);
            UserController controller = new UserController(mock.Object);

            // Act
            var result = controller.Post(new UserDTO());

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Update_the_task()
        {
            // Arrange
            var task = new UserDTO { User_Id = 1, FirstName = "fname", Employee_Id = 1 };
            mock.Setup(a => a.UpdateUser(task, task.User_Id)).Returns(true);
            UserController controller = new UserController(mock.Object);

            // Act

            var result = controller.Put(task.User_Id, task);

            // Assert
            Assert.AreEqual(true, result);
        }


        [Test]
        public void Delete_the_task()
        {
            // Arrange
            mock.Setup(a => a.DeleteUser(1)).Returns(true);
            UserController controller = new UserController(mock.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
