using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public interface IUserData
    {
        List<User> GetAllUsers();
        User GetUserByUserId(int userid);
        bool CreateUser(User user);
        bool UpdateUser(User user, int userid);
        bool DeleteUser(int userid);
        bool UpdateUserProjectIdTaskId(int userid,int?projectId,int?taskId);

    }
}
