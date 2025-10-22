using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICombatAttackTargetProvider : IProjectService
    {
        ICreatureController GetAttackTargetForCreature(CreatureType creatureType);
    }

    public class CombatAttackTargetProvider : ICombatAttackTargetProvider
    { 
        [Inject] private ICreatureService _creatureService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public ICreatureController GetAttackTargetForCreature(CreatureType creatureType)
        {
            ICreatureController result = null;

            switch (creatureType)
            {
                case CreatureType.PLAYER:
                    result = GetAttackTargetForPlayerCreature();
                    break;

                case CreatureType.ENEMY:
                    result = GetAttackTargetForEnemyCreature();
                    break;
            }

            return result;
        }

        private ICreatureController GetAttackTargetForPlayerCreature()
        {
            ICreatureController result = null;
            var allCreatureControllers = _creatureService.GetAllCreatureControllers();

            foreach (var creatureController in allCreatureControllers)
            {
                if (creatureController.CreatureType != CreatureType.ENEMY)
                    continue;

                var isAlive = creatureController.IsAlive();

                if (!isAlive) 
                    continue;

                result = creatureController;
            }

            return result;
        }

        private ICreatureController GetAttackTargetForEnemyCreature()
        {
            var result = _creatureService.PlayerController;
            return result;
        }
    }
}