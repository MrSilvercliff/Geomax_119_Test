using _Project.Scripts.Project.Services.Balance.Storages;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage.Async;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Balance
{
    public interface IProjectBalanceService : IBalanceServiceAbstractAsync
    {
        ICreatureBalanceStorage Creatures { get; }
        IAbilityBalanceStorage Abilities { get; }
    }

    public class ProjectBalanceService : BalanceServiceAbstractAsync, IProjectBalanceService
    {
        public ICreatureBalanceStorage Creatures { get; private set; }
        public IAbilityBalanceStorage Abilities { get; private set; }

        public ProjectBalanceService() 
        {
            Creatures = new CreatureBalanceStorage();
            Abilities = new AbilityBalanceStorage();
        }

        protected override HashSet<IProjectService> GetStoragesToInit()
        {
            var result = new HashSet<IProjectService>();

            result.Add(Creatures);
            result.Add(Abilities);

            return result;
        }
    }
}