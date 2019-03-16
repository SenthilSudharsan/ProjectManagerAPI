using AutoMapper;
using Moq;
using NUnit.Framework;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using ProjectManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;


namespace ProjectManagerAPI.Tests.UnitTests.BusinessTests
{
    [TestFixture]
    public class UserBusinessTest
    {
        Mock<IUserData> mockUser = new Mock<IUserData>();
        public UserBusinessTest()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TaskDTO, Task>().ForMember(dest => dest.Task1, opt => opt.MapFrom(src => src.Task))
                .ForMember(dest => dest.Task_Id, opt => opt.MapFrom(src => src.TaskId))
                .ForMember(dest => dest.Start_Date, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.End_Date, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Project_Id, opt => opt.MapFrom(src => src.Project_Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parent_Id, opt => opt.MapFrom(src => src.Parent_Id));
                cfg.CreateMap<ParentTask, ParentTaskDTO>();

                cfg.CreateMap<ParentTaskDTO, ParentTask>()
                .ForMember(dest => dest.Tasks, opt => opt.Ignore());
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Task, TaskDTO>()
                .ForMember(dest => dest.Task, opt => opt.MapFrom(src => src.Task1))
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Task_Id))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.Start_Date))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.End_Date))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
                .ForMember(dest => dest.Project_Id, opt => opt.MapFrom(src => src.Project_Id))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Parent_Id, opt => opt.MapFrom(src => src.Parent_Id))
                .ForMember(dest => dest.ParentDTOName, opt => opt.MapFrom(src => src.ParentTask.Parent_Task))
                .ForMember(dest => dest.ProjectDTOName, opt => opt.MapFrom(src => src.Project.Project1));

                cfg.CreateMap<ProjectDTO, Project>().ForMember(dest => dest.Project1, opt => opt.MapFrom(src => src.ProjectName));
                cfg.CreateMap<Project, ProjectDTO>().ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project1))
                .ForMember(dest => dest.TotalTaskCount, opt => opt.MapFrom(src => src.Tasks.Count))
                .ForMember(dest => dest.lstUsers, opt => opt.MapFrom(src => src.Users))
                .ForMember(dest => dest.CompletedTaskCount, opt => opt.MapFrom(src => src.Tasks.Where(a => a.Status.ToUpper().Trim() == "COMPLETED").Count()));

            });
        }
        [Test]
        public void get_all_users_from_repo()
        {
            mockUser.Setup(a => a.GetAllUsers()).Returns(new List<User> { new User { User_Id = 1, FirstName = "FName", Employee_Id = 1, LastName = "LName"} });
            UserBusiness appBusiness = new UserBusiness(mockUser.Object);

            // Act
            List<UserDTO> result = appBusiness.GetAllUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("FName", result.ElementAt(0).FirstName);
        }

        

        [Test]
        public void Get_user_By_Id_from_repo()
        {
            // Arrange
            mockUser.Setup(a => a.GetUserByUserId(1)).Returns(new User { User_Id = 1, FirstName = "FName", Employee_Id = 1, LastName = "LName" });
            UserBusiness appBusiness = new UserBusiness(mockUser.Object);

            // Act
            UserDTO result = appBusiness.GetUserByUserId(1);

            // Assert
            Assert.AreEqual("FName", result.FirstName);
        }

        [Test]
        public void Create_a_user_from_Repo()
        {
            // Arrange
            mockUser.Setup(a => a.CreateUser(It.IsAny<User>())).Returns(true);
            
            UserBusiness appBusiness = new UserBusiness(mockUser.Object);

            // Act
            var result = appBusiness.CreateUser(new UserDTO());

            // Assert
            Assert.AreEqual(true, result);
        }


        [Test]
        public void Update_the_user_from_repo()
        {
            // Arrange
            mockUser.Setup(a => a.UpdateUser(It.IsAny<User>(), 1)).Returns(true);
            mockUser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            UserBusiness appBusiness = new UserBusiness(mockUser.Object);

            // Act
            var result = appBusiness.UpdateUser(new UserDTO { User_Id = 1, FirstName = "FName", Employee_Id = 1, LastName = "LName" }, 1);

            // Assert
            Assert.AreEqual(true, result);
        }

        

        [Test]
        public void Delete_the_user_from_repo()
        {
            // Arrange
            mockUser.Setup(a => a.DeleteUser(1)).Returns(true);
            UserBusiness appBusiness = new UserBusiness(mockUser.Object);

            // Act
            var result = appBusiness.DeleteUser(1);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
