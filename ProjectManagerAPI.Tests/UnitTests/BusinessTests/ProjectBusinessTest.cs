using System;
using System.Linq;
using ProjectManager.Data;
using Moq;
using AutoMapper;
using ProjectManager.Business;
using ProjectManager.Business.DTO;
using NUnit.Framework;
using System.Collections.Generic;

namespace ProjectManagerAPI.Tests.UnitTests.BusinessTests
{
    [TestFixture]
    public class ProjectBusinessTest
    {
        Mock<IProjectData> mock = new Mock<IProjectData>();
        Mock<IUserData> mockUser = new Mock<IUserData>();
        public ProjectBusinessTest()
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
        public void get_all_projects_from_repo()
        {
            mock.Setup(a => a.GetAllProjects()).Returns(new List<Project> { new Project { Project_Id = 1, Project1 = "project", Priority = 1 } });
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockUser.Object);

            // Act
            List<ProjectDTO> result = appBusiness.GetAllProjects();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("project", result.ElementAt(0).ProjectName);
        }



        [Test]
        public void Get_project_By_Id_from_repo()
        {
            // Arrange
            mock.Setup(a => a.GetProjectByProjectId(1)).Returns(new Project { Project_Id = 1, Project1 = "project", Priority = 1 });
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockUser.Object);

            // Act
            ProjectDTO result = appBusiness.GetProjectByProjectId(1);

            // Assert
            Assert.AreEqual("project", result.ProjectName);
        }

        [Test]
        public void Create_a_project_from_Repo()
        {
            // Arrange
            mock.Setup(a => a.CreateProject(It.IsAny<Project>())).Returns(1);
            mockUser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);

            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.CreateProject(new ProjectDTO());

            // Assert
            Assert.AreEqual(true, result);
        }


        [Test]
        public void Update_the_project_from_repo()
        {
            // Arrange
            mock.Setup(a => a.UpdateProject(It.IsAny<Project>(), 1)).Returns(true);
            mockUser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.UpdateProject(new ProjectDTO { Project_Id = 1, ProjectName = "FName", Start_Date = DateTime.Now, End_Date = DateTime.Now }, 1);

            // Assert
            Assert.AreEqual(true, result);
        }



        [Test]
        public void Delete_the_project_from_repo()
        {
            // Arrange
            mock.Setup(a => a.DeleteProject(1)).Returns(true);
            ProjectBusiness appBusiness = new ProjectBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.DeleteProject(1);

            // Assert
            Assert.AreEqual(true, result);
        }

    }
}
