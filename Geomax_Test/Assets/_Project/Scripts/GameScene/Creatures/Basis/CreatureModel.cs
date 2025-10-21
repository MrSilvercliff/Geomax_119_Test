using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Basis
{
    public interface ICreatureModel
    {
        StateMachineType StateMachineType { get; }

        void Setup(IReadOnlyList<IAbility> abilities);
        IAbilityAttack GetBasicAttackAbility();
        T GetAbility<T>(string id) where T : IAbility;
    }

    public class CreatureModel : ICreatureModel
    {
        public StateMachineType StateMachineType => _balanceModel.StateMachineType;

        private ICreatureBalanceModel _balanceModel;
        private IReadOnlyList<IAbility> _abilities;

        public CreatureModel(ICreatureBalanceModel balanceModel)
        {
            _balanceModel = balanceModel;
        }

        public void Setup(IReadOnlyList<IAbility> abilities)
        {
            _abilities = abilities;
        }

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

            var result = (T)preResult;
            return result;
        }

        public class Factory : PlaceholderFactory<ICreatureBalanceModel, CreatureModel> { }
    }
}