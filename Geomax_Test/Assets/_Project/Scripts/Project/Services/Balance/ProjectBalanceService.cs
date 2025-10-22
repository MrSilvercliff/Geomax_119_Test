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
        ICreatureResourceBalanceStorage CreatureResources { get; }
        IAbilityBalanceStorage Abilities { get; }
        IEffectBalanceStorage Effects { get; }
    }

    public class ProjectBalanceService : BalanceServiceAbstractAsync, IProjectBalanceService
    {
        public ICreatureBalanceStorage Creatures { get; private set; }
        public ICreatureResourceBalanceStorage CreatureResources { get; private set; }
        public IAbilityBalanceStorage Abilities { get; private set; }
        public IEffectBalanceStorage Effects { get; private set; }

        public ProjectBalanceService() 
        {
            Creatures = new CreatureBalanceStorage();
            CreatureResources = new CreatureResourceBalanceStorage();
            Abilities = new AbilityBalanceStorage();
            Effects = new EffectBalanceStorage();
        }

        protected override HashSet<IProjectService> GetStoragesToInit()
        {
            var result = new HashSet<IProjectService>();

            result.Add(Creatures);
            result.Add(CreatureResources);
            result.Add(Abilities);
            result.Add(Effects);

            return result;
        }
    }
}