using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Enemy;
using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.GameScene.ObjectPools;
using _Project.Scripts.GameScene.Services.CreatureSlots;
using _Project.Scripts.Project.Enums;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureDespawnService : IProjectService
    { 
        void Despawn(ICreatureController controller);
    }

    public class CreatureDespawnService : ICreatureDespawnService
    {
        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;
        [Inject] private ICreatureSlotService _creatureSlotService;
        [Inject] private ICreatureSpawnService _creatureSpawnService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void Despawn(ICreatureController controller)
        {
            Debug.LogError($"Despawn creature {controller.InstanceID}");

            var creatureType = controller.CreatureType;

            switch (creatureType)
            {
                case CreatureType.PLAYER:
                    DespawnPlayerCreature(controller);
                    break;

                case CreatureType.ENEMY:
                    DespawnEnemyCreature(controller);
                    _creatureSpawnService.SpawnRandomEnemy();
                    break;

                default:
                    LogUtils.Error(this, $"Despawn for creature type [{creatureType}] not implemented!");
                    break;
            }
        }

        private void DespawnPlayerCreature(ICreatureController controller)
        {
            Debug.LogError($"Despawn player");

            SetCreatureSlotControllerFree(controller);
            DespawnPrefab((CreaturePrefab)controller.CreaturePrefab);
            var playerControllerPool = _gameSceneObjectPoolService.PlayerControllerPool;
            playerControllerPool.Despawn((PlayerController)controller);
        }

        private void DespawnEnemyCreature(ICreatureController controller)
        {
            Debug.LogError($"Despawn enemy");

            SetCreatureSlotControllerFree(controller);
            DespawnPrefab((CreaturePrefab)controller.CreaturePrefab);
            var enemyControllerPool = _gameSceneObjectPoolService.EnemyControllerPool;
            enemyControllerPool.Despawn((EnemyCreatureController)controller);
        }

        private void SetCreatureSlotControllerFree(ICreatureController controller)
        {
            var tryResult = _creatureSlotService.TryGetSlotWithCreatureController(controller, out var creatureSlotController);

            if (tryResult)
                creatureSlotController.SetCreatureController(null);
        }

        private void DespawnPrefab(CreaturePrefab creaturePrefab)
        {
            _gameSceneObjectPoolService.CreaturePrefabPool.Despawn(creaturePrefab);
        }
    }
}