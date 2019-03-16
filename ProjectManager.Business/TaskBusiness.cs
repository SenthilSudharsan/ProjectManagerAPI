using AutoMapper;
using ProjectManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ProjectManager.Business
{
    public class TaskBusiness : ITaskBusiness
    {
        ITaskData _taskData;
        IUserData _userData;
        public TaskBusiness(ITaskData taskData, IUserData userData)
        {
            _taskData = taskData;
            _userData = userData;
        }

        public bool CreateTask(TaskDTO task)
        {
            bool result = false;
            try
            {
                Data.Task taskDb = Mapper.Map<Data.Task>(task);
                if (task.IsParentTask)
                {
                    ParentTask parentTask = new ParentTask();
                    parentTask.Parent_Task = task.Task;
                    parentTask.Parent_Id = 0;
                    result = _taskData.CreateParentTask(parentTask);
                }
                else
                {
                    taskDb.ParentTask = null;
                    taskDb.Users = null;
                    taskDb.Project = null;
                    int generatedTaskId = _taskData.CreateTask(taskDb);
                    if (generatedTaskId > 0)
                    {
                        if (task.UserId != null && task.UserId > 0)
                        {
                            return _userData.UpdateUserProjectIdTaskId(Convert.ToInt32(task.UserId), task.Project_Id, generatedTaskId);
                        }
                        return true;
                    }
                    else
                        result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool DeleteTaskById(int taskId)
        {
            return _taskData.DeleteTaskById(taskId);
        }

        public bool EndTaskById(int taskId)
        {
            return _taskData.EndTaskById(taskId);
        }

        public List<ParentTaskDTO> GetParentTasks()
        {
            List<ParentTask> parentTasks = _taskData.GetParentTasks();
            List<ParentTaskDTO> parentDTOTasks = Mapper.Map<List<ParentTaskDTO>>(parentTasks);
            return parentDTOTasks;
        }

        public TaskDTO GetTaskById(int taskId)
        {
            Data.Task tasks = _taskData.GetTaskById(taskId);
            TaskDTO DTOTasks = Mapper.Map<TaskDTO>(tasks);
            return DTOTasks;
        }

        public List<TaskDTO> GetTasks()
        {
            List<Data.Task> tasks = _taskData.GetTasks();
            List<TaskDTO> DTOTasks = Mapper.Map<List<TaskDTO>>(tasks);
            return DTOTasks;
        }

        public bool UpdateTask(TaskDTO task, int taskId)
        {
            bool result = false;
            try
            {
                Task taskDb = Mapper.Map<Task>(task);
                taskDb.ParentTask = null;
                taskDb.Users = null;
                taskDb.Project = null;
                bool updatedTask = _taskData.UpdateTask(taskDb, taskId);
                if (updatedTask)
                {
                    if (task.UserId != null && task.UserId > 0)
                    {
                        return _userData.UpdateUserProjectIdTaskId(Convert.ToInt32(task.UserId), task.Project_Id, task.TaskId);
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
