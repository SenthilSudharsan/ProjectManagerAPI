using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public class UserData : IUserData
    {
        private static SqlProviderServices instance = SqlProviderServices.Instance;
        public ProjectManagerEntities _dbContext;

        public UserData()
        {
            _dbContext = new ProjectManagerEntities();
        }

        public bool CreateUser(User user)
        {
            bool result = false;
            try
            {
                user.User_Id = 0;
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteUser(int userid)
        {
            bool result = false;
            try
            {
                User user = _dbContext.Users.Where(a => a.User_Id == userid).FirstOrDefault();
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<User> GetAllUsers()
        {
            List<User> lstUsers = new List<User>();
            try
            {
                lstUsers = _dbContext.Users.ToList();

            }
            catch (Exception ex) { }
            return lstUsers;
        }

        public User GetUserByUserId(int userid)
        {
            User user = new User();
            try
            {
                user = _dbContext.Users.Where(a => a.User_Id == userid).FirstOrDefault();
            }
            catch (Exception ex)
            {
            }
            return user;
        }

        public bool UpdateUser(User user, int userid)
        {
            bool result = false;
            try
            {
                User userFromDB = _dbContext.Users.Where(a => a.User_Id == userid).FirstOrDefault();
                userFromDB.Employee_Id = user.Employee_Id;
                userFromDB.FirstName = user.FirstName;
                userFromDB.LastName = user.LastName;
                userFromDB.Project_Id = user.Project_Id;
                userFromDB.Task_Id = user.Task_Id;
                _dbContext.Entry(userFromDB).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;

            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool UpdateUserProjectIdTaskId(int userid, int? projectId, int? taskId)
        {
            bool result = false;
            try
            {
                if (projectId != null && projectId > 0)
                {
                    List<User> lstUsers = _dbContext.Users.Where(a => a.Project_Id == projectId).ToList();
                    foreach (var item in lstUsers)
                    {
                        item.Project_Id = null;
                        _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
                if (taskId != null && taskId > 0)
                {
                    List<User> lstUsers = _dbContext.Users.Where(a => a.Task_Id == taskId).ToList();
                    foreach (var item in lstUsers)
                    {
                        item.Task_Id = null;
                        _dbContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
                        _dbContext.SaveChanges();
                    }
                }
                User userFromDB = _dbContext.Users.Where(a => a.User_Id == userid).FirstOrDefault();
                userFromDB.Project_Id = projectId;
                userFromDB.Task_Id = taskId;
                _dbContext.Entry(userFromDB).State = System.Data.Entity.EntityState.Modified;
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
