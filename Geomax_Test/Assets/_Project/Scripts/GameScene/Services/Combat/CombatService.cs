using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
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

        ICreatureController GetSelectedCreatureController(CreatureType creatureType);
        void SelectCreatureController(ICreatureController creatureController);
    }

    public class CombatService : ICombatService
    {
        [Inject] private ICombatAbilityUseService _useAbilityService;
        [Inject] private ICombatAbilityResultApplyService _abilityResultApplyService;
        [Inject] private ICombatCreatureSelectService _creatureSelectService;

        public async Task<bool> Init()
        {
            await _useAbilityService.Init();
            await _abilityResultApplyService.Init();
            await _creatureSelectService.Init();
            return true;
        }

        public bool Flush()
        {
            _useAbilityService.Flush();
            _abilityResultApplyService.Flush();
            _creatureSelectService.Flush();
            return true;
        }

        public IAbilityResult UseAbility(ICreatureController creatureFrom, IAbility ability)
        {
            var result = _useAbilityService.UseAbility(creatureFrom, ability);
            return result;
        }

        public void ApplyAbilityResult(IAbilityResult abilityResult)
        {
            if (abilityResult == null)
                return;

            _abilityResultApplyService.ApplyAbilityResult(abilityResult);
        }

        public ICreatureController GetSelectedCreatureController(CreatureType creatureType)
        {
            var result = _creatureSelectService.GetSelectedCreatureController(creatureType);
            return result;
        }

        public void SelectCreatureController(ICreatureController creatureController)
        {
            _creatureSelectService.SelectCreatureController(creatureController);
        }
    }
}