using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Services.Abilities;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureModelCreator : IProjectService
    {
        ICreatureModel GetCreatureModel(string creatureId);
    }

    public class CreatureModelCreator : ICreatureModelCreator
    {
        [Inject] private IProjectBalanceService _projectBalanceStorage;
        [Inject] private CreatureModel.Factory _creatureModelFactory;
        [Inject] private IAbilityService _abilityService;
        [Inject] private CreatureResourceValue.Factory _creatureResourceFactory;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public ICreatureModel GetCreatureModel(string creatureId)
        {
            var creatureBalanceStorage = _projectBalanceStorage.Creatures;

            var tryGetResult = creatureBalanceStorage.TryGetById(creatureId, out var balanceModel);

            if (!tryGetResult)
            {
                LogUtils.Error(this, $"Creature balance model with id [{creatureId}] does not exist!");
                return null;
            }

            var creatureModel = _creatureModelFactory.Create(balanceModel);
            var abilities = GetAbilities(balanceModel);
            var resources = GetResources(balanceModel);
            creatureModel.Setup(abilities, resources);
            return creatureModel;
        }

        private IReadOnlyList<IAbility> GetAbilities(ICreatureBalanceModel balanceModel)
        {
            var result = new List<IAbility>();

            var abilityIds = balanceModel.Abilities;

            foreach (var abilityId in abilityIds)
            {
                var ability = _abilityService.GetAbility(abilityId);
                result.Add(ability);
            }

            return result;
        }

        private IReadOnlyDictionary<CreatureResourceType, ICreatureResourceValue> GetResources(ICreatureBalanceModel balanceModel)
        {
            var result = new Dictionary<CreatureResourceType, ICreatureResourceValue>();
            var creatureResourceBalanceStorage = _projectBalanceStorage.CreatureResources;
            var resourceIds = balanceModel.Resources;

            foreach (var resourceId in resourceIds) 
            {
                var tryResult = creatureResourceBalanceStorage.TryGetById(resourceId, out var resourceBalanceModel);

                if (!tryResult)
                    continue;

                var creatureResourceValue = _creatureResourceFactory.Create(resourceBalanceModel);
                result.TryAdd(creatureResourceValue.ResourceType, creatureResourceValue);
            }

            return result;
        }
    }
}