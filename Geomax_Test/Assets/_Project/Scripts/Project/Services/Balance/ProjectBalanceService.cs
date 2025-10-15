using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage.Async;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Balance
{
    public interface IProjectBalanceService : IBalanceServiceAbstractAsync
    { 
    }

    public class ProjectBalanceService : BalanceServiceAbstractAsync, IProjectBalanceService
    {
        public ProjectBalanceService() 
        {
        }

        protected override HashSet<IProjectService> GetStoragesToInit()
        {
            var result = new HashSet<IProjectService>();
            return result;
        }
    }
}