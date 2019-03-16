using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public class ProjectData : IProjectData
    {
        public ProjectManagerEntities _dbContext;
        public ProjectData()
        {
            _dbContext = new ProjectManagerEntities();
        }
        public int CreateProject(Project project)
        {
            int result = 0;
            try
            {
                project.Project_Id = 0;
                _dbContext.Projects.Add(project);
                _dbContext.SaveChanges();
                result = project.Project_Id;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }

        public bool DeleteProject(int projectid)
        {
            bool result = false;
            try
            {
                Project project = _dbContext.Projects.Where(a => a.Project_Id == projectid).FirstOrDefault();
                _dbContext.Projects.Remove(project);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> lstProject = new List<Project>();
            try
            {
              lstProject = _dbContext.Projects.Include("Users").Include("Tasks").ToList();

            }
            catch (Exception ex) { }
            return lstProject;
        }

        public Project GetProjectByProjectId(int projectid)
        {
            Project project = new Project();
            try
            {
                project = _dbContext.Projects.Where(a => a.Project_Id == projectid).FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            return project;
        }

        public bool UpdateProject(Project project, int projectid)
        {
            bool result = false;
            try
            {
                Project projectFromDB = _dbContext.Projects.Where(a => a.Project_Id == projectid).FirstOrDefault();
                projectFromDB.Project1 = project.Project1;
                projectFromDB.Priority = project.Priority;
                projectFromDB.End_Date = project.End_Date;
                projectFromDB.Start_Date = project.Start_Date;
                projectFromDB.Project_Id = project.Project_Id;
                _dbContext.Entry(projectFromDB).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        
    }
}
