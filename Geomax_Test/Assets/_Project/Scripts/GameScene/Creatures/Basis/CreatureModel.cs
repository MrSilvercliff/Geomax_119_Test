using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Creatures.Basis
{
    public interface ICreatureModel
    {
        StateMachineType StateMachineType { get; }

        void Setup(IReadOnlyList<IAbility> abilities, IReadOnlyDictionary<CreatureResourceType, ICreatureResourceValue> resources);



        #region RESOURCES

        ICreatureResourceValue GetResourceValue(CreatureResourceType resourceType);
        IReadOnlyCollection<ICreatureResourceValue> GetAllResources();

        #endregion RESOURCES



        #region ABILITIES

        IAbilityAttack GetBasicAttackAbility();
        T GetAbility<T>(string id) where T : IAbility;
        IReadOnlyList<IAbility> GetAllAbilities();

        #endregion ABILITIES
    }

    public class CreatureModel : ICreatureModel
    {
        public StateMachineType StateMachineType => _balanceModel.StateMachineType;

        private ICreatureBalanceModel _balanceModel;
        private IReadOnlyList<IAbility> _abilities;
        private IReadOnlyDictionary<CreatureResourceType, ICreatureResourceValue> _resources;

        public CreatureModel(ICreatureBalanceModel balanceModel)
        {
            _balanceModel = balanceModel;
        }

        public void Setup(IReadOnlyList<IAbility> abilities, IReadOnlyDictionary<CreatureResourceType, ICreatureResourceValue> resources)
        {
            _abilities = abilities;
            _resources = resources;
        }

        #region RESOURCES

        public ICreatureResourceValue GetResourceValue(CreatureResourceType resourceType)
        {
            var tryResult = _resources.TryGetValue(resourceType, out var result);

            if (!tryResult)
                LogUtils.Error(this, $"RESOURCE WITH TYPE {resourceType} DOES NOT EXIST!");

            return result;
        }

        public IReadOnlyCollection<ICreatureResourceValue> GetAllResources()
        {
            var result = _resources.Values.ToArray();
            return result;
        }

        #endregion RESOURCES



        #region ABILITIES

        public IAbilityAttack GetBasicAttackAbility()
        {
            var abilityId = _balanceModel.BasicAttackAbilityId;
            var result = GetAbility<IAbilityAttack>(abilityId);
            return result;
        }

        public T GetAbility<T>(string id) where T : IAbility
        {
            IAbility preResult = null;

            foreach (var ability in _abilities)
            {
                if (ability.Id == id)
                { 
                    preResult = ability;
                    break;
                }
            }

            if (preResult == null)
                return default;

            var result = (T)preResult;
            return result;
        }

        public IReadOnlyList<IAbility> GetAllAbilities()
        {
            return _abilities;
        }

        #endregion ABILITIES

        public class Factory : PlaceholderFactory<ICreatureBalanceModel, CreatureModel> { }
    }
}