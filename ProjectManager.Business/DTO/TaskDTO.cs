using ProjectManager.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Business
{
    public class TaskDTO
    {
        public int TaskId { get; set; }
        public Nullable<int> Parent_Id { get; set; }
        public Nullable<int> Project_Id { get; set; }
        public string Task { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public int Priority { get; set; }
        public string Status { get; set; }
        public Nullable<int> UserId { get; set; }
        public bool IsParentTask { get; set; }
        public string ParentDTOName { get; set; }
        public string ProjectDTOName { get; set; }
        public bool IsTaskEnded
        {
            get
            {
                if (EndDate <= DateTime.Now.Date)
                    return true;
                else
                    return false;
            }
        }

        public ParentTaskDTO ParentTask { get; set; }
        public ProjectDTO Project { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
