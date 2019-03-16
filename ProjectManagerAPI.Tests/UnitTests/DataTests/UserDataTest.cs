using System;
using Moq;
using ProjectManager.Data;
using System.Linq;
using NUnit.Framework;

namespace ProjectManagerAPI.Tests.UnitTests.DataTests
{
    [TestFixture]
    public class UserDataTest
    {
        Mock<ProjectManagerEntities> mock = new Mock<ProjectManagerEntities>();
        //Task task;
        //ParentTask parentTask;
        //Project project;
        User user;
        UserData userData;
        public UserDataTest()
        {
            userData = new UserData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            
            
        }

        [Test]
        public void Create_a_user_in_db()
        {
            // Arrange
            UserData appRepository = new UserData();

            // Act
            var result = appRepository.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void Get_all_user_Fom_Db()
        {

            //UserData appRepository = new UserData();

            var result = userData.GetAllUsers();

            Assert.IsNotNull(result);
        }


        
        [Test]
        public void Get_user_By_Id_in_db()
        {
            // Arrange
            //TaskData appRepository = new TaskData();
            UserData userData = new UserData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            // Act
            var result = userData.GetUserByUserId(user.User_Id);

            // Assert
            Assert.NotNull(result);
        }



        [Test]
        public void Update_the_user_in_db()
        {
            // Arrange
            UserData userData = new UserData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            // Act
            var result = userData.UpdateUser(user, user.User_Id);

            // Assert
            Assert.AreEqual(true, result);
        }

        

        [Test]
        public void Delete_the_user_in_db()
        {
            // Arrange
            UserData userData = new UserData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            // Act
            var result = userData.DeleteUser(user.User_Id);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
