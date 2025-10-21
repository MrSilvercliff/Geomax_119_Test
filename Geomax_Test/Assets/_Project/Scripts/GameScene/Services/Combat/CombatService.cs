using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Combat
{
    public interface ICombatService : IProjectService
    {
        IAbilityResult UseAbility(ICreatureController creatureFrom, IAbility ability);
        void ApplyAbilityResult(IAbilityResult abilityResult);
    }

    public class CombatService : ICombatService
    {
        [Inject] private ICombatAbilityUseService _useAbilityService;
        [Inject] private ICombatAbilityResultApplyService _abilityResultApplyService;

        public async Task<bool> Init()
        {
            await _useAbilityService.Init();
            await _abilityResultApplyService.Init();
            return true;
        }

        public bool Flush()
        {
            _useAbilityService.Flush();
            _abilityResultApplyService.Flush();
            return true;
        }

        public IAbilityResult UseAbility(ICreatureController creatureFrom, IAbility ability)
        {
            var result = _useAbilityService.UseAbility(creatureFrom, ability);
            return result;
        }

        public void ApplyAbilityResult(IAbilityResult abilityResult)
        {
            _abilityResultApplyService.ApplyAbilityResult(abilityResult);
        }
    }
}