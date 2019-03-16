using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public interface ITaskData
    {
        List<Task> GetTasks();
        Task GetTaskById(int taskId);
        bool UpdateTask(Task task, int taskId);
        bool DeleteTaskById(int taskId);
        bool EndTaskById(int taskId);
        int CreateTask(Task task);

        List<ParentTask> GetParentTasks();
        bool CreateParentTask(ParentTask parentTask);
    }
}
