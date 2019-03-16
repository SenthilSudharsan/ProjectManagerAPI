using ProjectManager.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Business
{
    public interface IProjectBusiness
    {
        List<ProjectDTO> GetAllProjects();
        ProjectDTO GetProjectByProjectId(int projectid);
        bool CreateProject(ProjectDTO project);
        bool UpdateProject(ProjectDTO project, int projectid);
        bool DeleteProject(int projectid);
    }
}
