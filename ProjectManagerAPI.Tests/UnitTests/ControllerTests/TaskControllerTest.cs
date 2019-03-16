using Moq;
using NUnit.Framework;
using ProjectManager.Business;
using ProjectManagerAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManagerAPI.Tests.UnitTests
{
    [TestFixture]
    public class TaskControllerTest
    {
        Mock<ITaskBusiness> mock = new Mock<ITaskBusiness>();


        [Test]
        public void Get_All_Tasks()
        {
            // Arrange

            mock.Setup(a => a.GetTasks()).Returns(new List<TaskDTO> { new TaskDTO { TaskId = 1, Task = "TestTask", Priority = 1, StartDate = DateTime.Now.Date } });
            TaskController controller = new TaskController(mock.Object);

            // Act
            IEnumerable<TaskDTO> result = controller.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TestTask", result.ElementAt(0).Task);
        }

        [Test]
        public void Get_All_Parent_Tasks()
        {
            // Arrange

            mock.Setup(a => a.GetParentTasks()).Returns(new List<ParentTaskDTO> { new ParentTaskDTO { Parent_Id = 1, Parent_Task = "Test parent Task" } });
            TaskController controller = new TaskController(mock.Object);

            // Act
            IEnumerable<ParentTaskDTO> result = controller.GetParents();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test parent Task", result.ElementAt(0).Parent_Task);
        }

        [Test]
        public void Get_Task_By_Id()
        {
            // Arrange
            mock.Setup(a => a.GetTaskById(1)).Returns(new TaskDTO { TaskId = 1, Task = "TestTask", Priority = 1, StartDate = DateTime.Now.Date });
            TaskController controller = new TaskController(mock.Object);

            // Act
            TaskDTO result = controller.Get(1);

            // Assert
            Assert.AreEqual("TestTask", result.Task);
        }

        [Test]
        public void Create_a_task()
        {
            // Arrange
            mock.Setup(a => a.CreateTask(It.IsAny<TaskDTO>())).Returns(true);
            TaskController controller = new TaskController(mock.Object);

            // Act
            var result = controller.Post(new TaskDTO());

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Update_the_task()
        {
            // Arrange
            var task = new TaskDTO { TaskId = 1, Task = "TestTask", Priority = 1 };
            mock.Setup(a => a.UpdateTask(task, task.TaskId)).Returns(true);
            TaskController controller = new TaskController(mock.Object);

            // Act
            
            var result = controller.update(task, task.TaskId);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void End_the_task()
        {
            // Arrange
            mock.Setup(a => a.EndTaskById(1)).Returns(true);
            TaskController controller = new TaskController(mock.Object);

            // Act
            var result = controller.End(1);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Delete_the_task()
        {
            // Arrange
            mock.Setup(a => a.DeleteTaskById(1)).Returns(true);
            TaskController controller = new TaskController(mock.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
