using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using ProjectManagerAPI;
using ProjectManagerAPI.Controllers;
using NUnit.Framework;
using Moq;
using ProjectManager.Business;
using ProjectManager.Business.DTO;

namespace ProjectManagerAPI.Tests.Controllers
{
    [TestFixture]
    public class ProjectControllerTest
    {
        Mock<IProjectBusiness> mock = new Mock<IProjectBusiness>();
        [Test]
        public void Get_Projects()
        {
            //Arrange
            mock.Setup(a => a.GetAllProjects()).Returns(new List<ProjectDTO> { new ProjectDTO { Project_Id = 1, ProjectName = "TestProject", Start_Date = DateTime.Now, Priority = 1 } });
            ProjectController controller = new ProjectController(mock.Object);

            // Act
            IEnumerable<ProjectDTO> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("TestProject", result.ElementAt(0).ProjectName);
        }

        [Test]
        public void GetProjectById()
        {
            //Arrange
            mock.Setup(a => a.GetProjectByProjectId(1)).Returns(new ProjectDTO { Project_Id = 1, ProjectName = "TestProject", Start_Date = DateTime.Now, Priority = 1 });
            ProjectController controller = new ProjectController(mock.Object);

            // Act
            var result = controller.Get(1);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void Post_project()
        {
            //Arrange
            var DTO = new ProjectDTO { Project_Id = 1, ProjectName = "TestProject", Start_Date = DateTime.Now, Priority = 1 };
            mock.Setup(a => a.CreateProject(DTO)).Returns(true);
            ProjectController controller = new ProjectController(mock.Object);

            // Act
            var result = controller.Post(DTO);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Put_project()
        {
            //Arrange
            var dto = new ProjectDTO { Project_Id = 1, ProjectName = "TestProject", Start_Date = DateTime.Now, Priority = 1 };
            mock.Setup(a => a.UpdateProject(dto, dto.Project_Id)).Returns(true);
            ProjectController controller = new ProjectController(mock.Object);

            // Act
            var result = controller.Put(dto.Project_Id, dto);

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void Delete_project()
        {
            //Arrange
            var dto = new ProjectDTO { Project_Id = 1, ProjectName = "TestProject", Start_Date = DateTime.Now, Priority = 1 };
            mock.Setup(a => a.DeleteProject(1)).Returns(true);
            ProjectController controller = new ProjectController(mock.Object);

            // Act
            var result = controller.Delete(1);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
