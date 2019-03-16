using ProjectManager.Business;
using ProjectManager.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagerAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProjectController : ApiController
    {
        IProjectBusiness _projectBusiness;
        public ProjectController(IProjectBusiness projectBusiness)
        {
            _projectBusiness = projectBusiness;
        }

        // GET api/values
        public IEnumerable<ProjectDTO> Get()
        {
            return _projectBusiness.GetAllProjects();
        }

        // GET api/values/5
        public ProjectDTO Get(int id)
        {
            return _projectBusiness.GetProjectByProjectId(id);
        }

        // POST api/values
        public bool Post([FromBody]ProjectDTO value)
        {
            return _projectBusiness.CreateProject(value);
        }

        // PUT api/values/5
        public bool Put(int id, [FromBody]ProjectDTO value)
        {
            return _projectBusiness.UpdateProject(value, id);
        }

        // DELETE api/values/5
        public bool Delete(int id)
        {
            return _projectBusiness.DeleteProject(id);
        }
    }
}
