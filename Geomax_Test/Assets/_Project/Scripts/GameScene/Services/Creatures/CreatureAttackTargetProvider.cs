using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureAttackTargetProvider : IProjectService
    {
        ICreatureController GetAttackTargetForPlayerCreature();
        ICreatureController GetAttackTargetForEnemyCreature();
    }

    public class CreatureAttackTargetProvider : ICreatureAttackTargetProvider
    { 
        [Inject] private ICreatureControllerRepository _creatureControllerRepository;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public ICreatureController GetAttackTargetForPlayerCreature()
        {
            ICreatureController result = null;
            var allCreatureControllers = _creatureControllerRepository.GetAll();

            foreach (var creatureController in allCreatureControllers)
            {
                if (creatureController.CreatureType == CreatureType.ENEMY)
                {
                    result = creatureController;
                    break;
                }
            }

            return result;
        }

        public ICreatureController GetAttackTargetForEnemyCreature()
        {
            var result = _creatureControllerRepository.PlayerController;
            return result;
        }
    }
}