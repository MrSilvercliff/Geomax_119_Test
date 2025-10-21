using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.Abilities
{
    public interface IAbilitiesCreator : IProjectService
    {
        IAbility CreateAbility(string id);
        IAbility CreateAbility(IAbilityBalanceModel balanceModel);
    }

    public class AbilitiesCreator : IAbilitiesCreator
    {
        [Inject] private IProjectBalanceService _projectBalanceStorage;

        [Inject] private AbilityAttack.Factory _attackFactory;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public IAbility CreateAbility(string id)
        {
            var abilitiesBalanceStorage = _projectBalanceStorage.Abilities;
            var tryGetResult = abilitiesBalanceStorage.TryGetById(id, out var balanceModel);

            if (!tryGetResult)
            {
                LogUtils.Error(this, $"Ability balance model with id [{id}] does not exist!");
                return null;
            }

            var result = CreateProcess(balanceModel);
            return result;
        }

        public IAbility CreateAbility(IAbilityBalanceModel balanceModel)
        {
            var result = CreateAbility(balanceModel);
            return result;
        }

        private IAbility CreateProcess(IAbilityBalanceModel balanceModel)
        {
            var abilityType = balanceModel.AbilityType;

            IAbility result = null;

            switch (abilityType)
            {
                case AbilityType.BASIC_ATTACK:
                    result = _attackFactory.Create(balanceModel);
                    break;

                default:
                    LogUtils.Error(this, $"Ability factory for ability type [{abilityType}] does not implemented!");
                    break;
            }

            return result;
        }
    }
}