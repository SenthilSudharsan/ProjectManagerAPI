using ProjectManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Business.DTO;
using AutoMapper;

namespace ProjectManager.Business
{
    public class ProjectBusiness : IProjectBusiness
    {
        IProjectData _projectData;
        IUserData _userData;
        public ProjectBusiness(IProjectData projectData, IUserData userData)
        {
            _projectData = projectData;
            _userData = userData;
        }

        public bool CreateProject(ProjectDTO project)
        {
            bool result = false;
            try
            {
                Project projectDb = Mapper.Map<Project>(project);
                int generatedProjectId = _projectData.CreateProject(projectDb);
                if (generatedProjectId > 0)
                {
                    if (project.UserId != null && project.UserId > 0)
                    {
                        return _userData.UpdateUserProjectIdTaskId(Convert.ToInt32(project.UserId), generatedProjectId, null);
                    }
                    return true;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteProject(int projectid)
        {
           return _projectData.DeleteProject(projectid);
        }

        public List<ProjectDTO> GetAllProjects()
        {
            List<ProjectDTO> dto = new List<ProjectDTO>();
            var result = _projectData.GetAllProjects();
            dto = Mapper.Map<List<ProjectDTO>>(result);
            return dto;
        }

        public ProjectDTO GetProjectByProjectId(int projectid)
        {
            var result = _projectData.GetProjectByProjectId(projectid);
            ProjectDTO projectDto = Mapper.Map<ProjectDTO>(result);
            return projectDto;
        }

        public bool UpdateProject(ProjectDTO project, int projectid)
        {
            bool result = false;
            try
            {
                Project projectDb = Mapper.Map<Project>(project);
                bool updatedProject = _projectData.UpdateProject(projectDb,projectid);
                if (updatedProject)
                {
                    if (project.UserId != null && project.UserId > 0)
                    {
                        return _userData.UpdateUserProjectIdTaskId(Convert.ToInt32(project.UserId), projectid, null);
                    }
                    return true;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
