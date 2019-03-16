using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Data
{
    public class TaskData : ITaskData
    {
        public ProjectManagerEntities _dbContext;
        public TaskData()
        {
            _dbContext = new ProjectManagerEntities();
        }
        public bool CreateParentTask(ParentTask parentTask)
        {
            try
            {
                _dbContext.ParentTasks.Add(parentTask);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int CreateTask(Task task)
        {
            int createdTask = 0;
            try
            {
                task.Task_Id = 0;
                task.Status = "STARTED";
                _dbContext.Tasks.Add(task);
                _dbContext.SaveChanges();
                createdTask = task.Task_Id;
            }
            catch (Exception ex)
            {
                createdTask = 0;
            }
            return createdTask;
        }

        public bool DeleteTaskById(int taskId)
        {
            bool result = false;
            try
            {
                Task task = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
                _dbContext.Tasks.Remove(task);
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public bool EndTaskById(int taskId)
        {
            bool result = false;
            try
            {
                Task task = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
                task.End_Date = DateTime.Now.Date;
                task.Status = "COMPLETED";
                _dbContext.Entry(task).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public List<ParentTask> GetParentTasks()
        {
            List<ParentTask> tasks = new List<ParentTask>();
            try
            {
                tasks = _dbContext.ParentTasks.ToList();
            }
            catch (Exception ex) { }
            return tasks;
        }

        public Task GetTaskById(int taskId)
        {
            Task task = new Task();
            try
            {
                task = _dbContext.Tasks.Include("ParentTask").Include("Project").Include("Users").Where(a => a.Task_Id == taskId).FirstOrDefault();
            }
            catch (Exception ex) { }
            return task;
        }

        public List<Task> GetTasks()
        {
            List<Task> tasks = new List<Task>();
            try
            {
                tasks = _dbContext.Tasks.Include("ParentTask").Include("Project").Include("Users").ToList();
            }
            catch (Exception ex) { }
            return tasks;
        }

        public bool UpdateTask(Task task, int taskId)
        {
            bool result = false;
            try
            {
                Task taskFromDb = _dbContext.Tasks.Where(a => a.Task_Id == taskId).FirstOrDefault();
                taskFromDb.End_Date = task.End_Date;
                taskFromDb.Start_Date = task.Start_Date;
                taskFromDb.Parent_Id = task.Parent_Id;
                taskFromDb.Project_Id = task.Project_Id;
                taskFromDb.Priority = task.Priority;
                taskFromDb.Task1 = task.Task1;
                taskFromDb.Status = task.Status;
                _dbContext.Entry(taskFromDb).State = System.Data.Entity.EntityState.Modified;
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
