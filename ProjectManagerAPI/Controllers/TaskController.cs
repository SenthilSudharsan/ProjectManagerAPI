using ProjectManager.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ProjectManagerAPI.Controllers
{
    [RoutePrefix("Task")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ApiController
    {
        ITaskBusiness _taskBusiness;

        public TaskController(ITaskBusiness taskBusiness)
        {
            _taskBusiness = taskBusiness;
        }

        [Route("getAll")]
        public IEnumerable<TaskDTO> GetAll()
        {
            return _taskBusiness.GetTasks();
        }

        [Route("get/{id}")]
        public TaskDTO Get(int id)
        {
            return _taskBusiness.GetTaskById(id);
        }

        // POST api/values
        [Route("create")]
        public bool Post([FromBody]TaskDTO value)
        {
           return _taskBusiness.CreateTask(value);
        }

        // PUT api/values/5

        [Route("update/{id}")]
        [HttpPost]
        public bool update([FromBody]TaskDTO value, int id)
        {
            return _taskBusiness.UpdateTask(value, value.TaskId);
        }

        // DELETE api/values/5
        [Route("delete/{id}")]
        public bool Delete(int id)
        {
            return _taskBusiness.DeleteTaskById(id);
        }

        // END api/values/end/5
        [Route("end/{id}")]
        [HttpGet]
        public bool End(int id)
        {
            return _taskBusiness.EndTaskById(id);
        }

        // GETPARENT api/values/parents/hello world
        [Route("getParents")]
        [HttpGet]
        public IEnumerable<ParentTaskDTO> GetParents()
        {
            return _taskBusiness.GetParentTasks();
        }

    }
}