using System;
using AutoMapper;
using Moq;
using NUnit.Framework;
using ProjectManager.Business;
using ProjectManager.Data;
using ProjectManager.Business.DTO;
using System.Linq;
using System.Collections.Generic;

namespace ProjectManagerAPI.Tests.UnitTests.BusinessTests
{
    [TestFixture]
    public class TaskBusinessTest
    {
        Mock<ITaskData> mock = new Mock<ITaskData>();
        Mock<IUserData> mockUser = new Mock<IUserData>();

        public TaskBusinessTest()
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
        public void get_all_task_from_repo()
        {
            mock.Setup(a => a.GetTasks()).Returns(new List<Task> { new Task { Task_Id = 1, Task1 = "TestTask", Priority = 1, Start_Date = DateTime.Now.Date } });
            TaskBusiness appBusiness = new TaskBusiness(mock.Object,mockUser.Object);

            // Act
            List<TaskDTO> result = appBusiness.GetTasks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TestTask", result.ElementAt(0).Task);
        }

        [Test]
        public void Get_All_Parent_Tasks_from_Repo()
        {
            // Arrange

            mock.Setup(a => a.GetParentTasks()).Returns(new List<ParentTask> { new ParentTask { Parent_Id = 1, Parent_Task = "Test parent Task" } });
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            IEnumerable<ParentTaskDTO> result = appBusiness.GetParentTasks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test parent Task", result.ElementAt(0).Parent_Task);
        }

        [Test]
        public void Get_Task_By_Id_from_repo()
        {
            // Arrange
            mock.Setup(a => a.GetTaskById(1)).Returns(new Task { Task_Id = 1, Task1 = "TestTask", Priority = 1, Start_Date = DateTime.Now.Date });
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            TaskDTO result = appBusiness.GetTaskById(1);

            // Assert
            Assert.AreEqual("TestTask", result.Task);
        }

        [Test]
        public void Create_a_task_from_Repo()
        {
            // Arrange
            mock.Setup(a => a.CreateTask(It.IsAny<Task>())).Returns(1);
            mockUser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.CreateTask(new TaskDTO());

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Create_a_parent_task_from_Repo()
        {
            // Arrange
            mock.Setup(a => a.CreateParentTask(It.IsAny<ParentTask>())).Returns(true);
            mockUser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            var task = new TaskDTO();
            task.IsParentTask = true;
            var result = appBusiness.CreateTask(task);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Update_the_task_from_repo()
        {
            // Arrange
            mock.Setup(a => a.UpdateTask(It.IsAny<Task>(), 1)).Returns(true);
            mockUser.Setup(a => a.UpdateUserProjectIdTaskId(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.UpdateTask(new TaskDTO { TaskId = 1, Task = "TestTask", Priority = 1, StartDate = DateTime.Now.Date }, 1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void End_the_task_from_repo()
        {
            // Arrange
            mock.Setup(a => a.EndTaskById(1)).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.EndTaskById(1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Delete_the_task_from_repo()
        {
            // Arrange
            mock.Setup(a => a.DeleteTaskById(1)).Returns(true);
            TaskBusiness appBusiness = new TaskBusiness(mock.Object, mockUser.Object);

            // Act
            var result = appBusiness.DeleteTaskById(1);

            // Assert
            Assert.AreEqual(true, result);
        }
    }

    
}
