using Moq;
using NUnit.Framework;
using ProjectManager.Data;
using System;
using System.Linq;

namespace ProjectManagerAPI.Tests.UnitTests.DataTests
{
    [TestFixture]
    public class TaskDataTest
    {
        Mock<ProjectManagerEntities> mock = new Mock<ProjectManagerEntities>();
        Task task;
        ParentTask parentTask;
        Project project;
        User user;
        public TaskDataTest()
        {
            TaskData appRepository = new TaskData();
            UserData userData = new UserData();
            ProjectData projectData = new ProjectData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            projectData.CreateProject(new Project { Project1 = "TestProject", Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestProject").FirstOrDefault();
            appRepository.CreateParentTask(new ParentTask { Parent_Task = "TestTaskParent" });
            parentTask = appRepository._dbContext.ParentTasks.Where(a => a.Parent_Task == "TestTaskParent").FirstOrDefault();
            appRepository.CreateTask(new Task() { Task1 = "TestTask", Priority = 1, Start_Date = DateTime.Now.Date, End_Date = DateTime.Now.Date.AddDays(1), Parent_Id = parentTask.Parent_Id, Project_Id = project.Project_Id });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask").FirstOrDefault();
        }

        [Test]
        public void Create_a_task_in_db()
        {
            // Arrange
            TaskData appRepository = new TaskData();

            // Act
            var result = appRepository.CreateTask(new Task() { Task1 = "TestTask2", Priority = 1, Start_Date = DateTime.Now.Date, End_Date = DateTime.Now.Date.AddDays(1) });

            // Assert
            Assert.NotZero(result);
        }

        [Test]
        public void Get_all_task_Fom_Db()
        {

            mock.Setup(x => x.Tasks.Add(It.IsAny<Task>())).Returns((Task u) => u);
            TaskData appRepository = new TaskData();

            var result = appRepository.GetTasks();

            Assert.IsNotNull(result);
        }


        [Test]
        public void Get_All_Parent_Tasks_in_db()
        {
            // Arrange

            TaskData appRepository = new TaskData();

            // Act
            var result = appRepository.GetParentTasks();

            // Assert
            Assert.IsNotNull(result);

        }

        [Test]
        public void Get_Task_By_Id_in_db()
        {
            // Arrange
            TaskData appRepository = new TaskData();

            // Act
            var result = appRepository.GetTaskById(task.Task_Id);

            // Assert
            Assert.NotNull(result);
        }



        [Test]
        public void Update_the_task_in_db()
        {
            // Arrange
            TaskData appRepository = new TaskData();
            UserData userData = new UserData();
            ProjectData projectData = new ProjectData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            projectData.CreateProject(new Project { Project1 = "TestProject", Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestProject").FirstOrDefault();
            appRepository.CreateParentTask(new ParentTask { Parent_Task = "TestTaskParent" });
            parentTask = appRepository._dbContext.ParentTasks.Where(a => a.Parent_Task == "TestTaskParent").FirstOrDefault();
            appRepository.CreateTask(new Task() { Task1 = "TestTask", Priority = 1, Start_Date = DateTime.Now.Date, End_Date = DateTime.Now.Date.AddDays(1), Parent_Id = parentTask.Parent_Id, Project_Id = project.Project_Id });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask").FirstOrDefault();
            // Act
            var result = appRepository.UpdateTask(task, task.Task_Id);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void End_the_task_in_db()
        {
            // Arrange
            TaskData appRepository = new TaskData();
            UserData userData = new UserData();
            ProjectData projectData = new ProjectData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            projectData.CreateProject(new Project { Project1 = "TestProject", Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestProject").FirstOrDefault();
            appRepository.CreateParentTask(new ParentTask { Parent_Task = "TestTaskParent" });
            parentTask = appRepository._dbContext.ParentTasks.Where(a => a.Parent_Task == "TestTaskParent").FirstOrDefault();
            appRepository.CreateTask(new Task() { Task1 = "TestTask", Priority = 1, Start_Date = DateTime.Now.Date, End_Date = DateTime.Now.Date.AddDays(1), Parent_Id = parentTask.Parent_Id, Project_Id = project.Project_Id });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask").FirstOrDefault();
            // Act
            var result = appRepository.EndTaskById(task.Task_Id);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Delete_the_task_in_db()
        {
            // Arrange
            TaskData appRepository = new TaskData();
            UserData userData = new UserData();
            ProjectData projectData = new ProjectData();
            userData.CreateUser(new User { FirstName = "TestFname", LastName = "TestLname", Employee_Id = 1 });
            user = userData._dbContext.Users.Where(a => a.Employee_Id == 1).FirstOrDefault();
            projectData.CreateProject(new Project { Project1 = "TestProject", Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestProject").FirstOrDefault();
            appRepository.CreateParentTask(new ParentTask { Parent_Task = "TestTaskParent" });
            parentTask = appRepository._dbContext.ParentTasks.Where(a => a.Parent_Task == "TestTaskParent").FirstOrDefault();
            appRepository.CreateTask(new Task() { Task1 = "TestTask", Priority = 1, Start_Date = DateTime.Now.Date, End_Date = DateTime.Now.Date.AddDays(1), Parent_Id = parentTask.Parent_Id, Project_Id = project.Project_Id });
            task = appRepository._dbContext.Tasks.Where(a => a.Task1 == "TestTask").FirstOrDefault();
            // Act
            var result = appRepository.DeleteTaskById(task.Task_Id);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
