using ProjectManager.Business;
using ProjectManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Unity;

namespace ProjectManagerAPI
{
    public class UnityConfig
    {
        public static void RegisterContainers(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<IProjectData, ProjectData>();
            container.RegisterType<ITaskData, TaskData>();
            container.RegisterType<IUserData, UserData>();
            container.RegisterType<IProjectBusiness, ProjectBusiness>();
            container.RegisterType<ITaskBusiness, TaskBusiness>();
            container.RegisterType<IUserBusiness, UserBusiness>();
            container.Resolve<ProjectBusiness>();
            container.Resolve<TaskBusiness>();
            container.Resolve<UserBusiness>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}