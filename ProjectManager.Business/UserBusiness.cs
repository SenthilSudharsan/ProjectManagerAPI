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
    public class UserBusiness : IUserBusiness
    {
        IUserData _userData;
        public UserBusiness(IUserData userData)
        {
            _userData = userData;
        }

        public bool CreateUser(UserDTO user)
        {
            User userDb = Mapper.Map<User>(user);
            return _userData.CreateUser(userDb);
        }

        public bool DeleteUser(int userid)
        {
            return _userData.DeleteUser(userid);
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> dto = new List<UserDTO>();
            var result = _userData.GetAllUsers();
            dto = Mapper.Map<List<UserDTO>>(result);
            return dto;
        }

        public UserDTO GetUserByUserId(int userid)
        {
            var result = _userData.GetUserByUserId(userid);
            UserDTO userDto = Mapper.Map<UserDTO>(result);
            return userDto;

        }

        public bool UpdateUser(UserDTO user, int userid)
        {
            User userDb = Mapper.Map<User>(user);
            return _userData.UpdateUser(userDb, userid);
        }
    }
}
