using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public interface IProjectData
    {
        List<Project> GetAllProjects();
        Project GetProjectByProjectId(int projectid);
        int CreateProject(Project project);
        bool UpdateProject(Project project, int projectid);
        bool DeleteProject(int projectid);
    }
}
