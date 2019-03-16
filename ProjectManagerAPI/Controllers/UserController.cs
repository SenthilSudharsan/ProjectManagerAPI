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
    public class UserController : ApiController
    {
        IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        // GET api/<controller>
        public IEnumerable<UserDTO> Get()
        {
            return _userBusiness.GetAllUsers();
                 
        }

        // GET api/<controller>/5
        public UserDTO Get(int id)
        {
            return _userBusiness.GetUserByUserId(id);
        }

        // POST api/<controller>
        public bool Post([FromBody]UserDTO value)
        {
            return _userBusiness.CreateUser(value);
        }

        // PUT api/<controller>/5
        public bool Put(int id, [FromBody]UserDTO value)
        {
            return _userBusiness.UpdateUser(value, id);
        }

        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            return _userBusiness.DeleteUser(id);
        }
    }
}