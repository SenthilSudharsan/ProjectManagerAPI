using System;

using NUnit.Framework;
using Moq;
using ProjectManager.Data;
using System.Linq;

namespace ProjectManagerAPI.Tests.UnitTests.DataTests
{
    [TestFixture]
    public class ProjectDataTest
    {
        Mock<ProjectManagerEntities> mock = new Mock<ProjectManagerEntities>();
        //Task task;
        //ParentTask parentTask;
        Project project;
        
        ProjectData projectData;
        public ProjectDataTest()
        {
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "TestFname", Start_Date = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestFname").FirstOrDefault();


        }

        [Test]
        public void Create_a_project_in_db()
        {
            // Arrange
            ProjectData projectData = new ProjectData();

            // Act
            var result = projectData.CreateProject(new Project { Project1 = "TestFname", Start_Date = DateTime.Now, Priority = 1 });

            // Assert
            Assert.IsTrue(result>0);
        }

        [Test]
        public void Get_all_project_Fom_Db()
        {

            ProjectData projectData = new ProjectData();

            var result = projectData.GetAllProjects();

            Assert.IsNotNull(result);
        }



        [Test]
        public void Get_project_By_Id_in_db()
        {
            // Arrange
            //TaskData appRepository = new TaskData();
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "TestFname", Start_Date = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestFname").FirstOrDefault();
            // Act
            var result = projectData.GetProjectByProjectId(project.Project_Id);

            // Assert
            Assert.NotNull(result);
        }



        [Test]
        public void Update_the_project_in_db()
        {
            // Arrange
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "TestFname", Start_Date = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestFname").FirstOrDefault();
            // Act
            var result = projectData.UpdateProject(project, project.Project_Id);

            // Assert
            Assert.AreEqual(true, result);
        }



        [Test]
        public void Delete_the_project_in_db()
        {
            // Arrange
            ProjectData projectData = new ProjectData();
            projectData.CreateProject(new Project { Project1 = "TestFname", Start_Date = DateTime.Now, Priority = 1 });
            project = projectData._dbContext.Projects.Where(a => a.Project1 == "TestFname").FirstOrDefault();
            // Act
            var result = projectData.DeleteProject(project.Project_Id);

            // Assert
            Assert.AreEqual(true, result);
        }
    }
}
