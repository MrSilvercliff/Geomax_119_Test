using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.GameScene.CreatureSlot;
using _Project.Scripts.GameScene.ObjectPools;
using _Project.Scripts.GameScene.Services.CreatureSlots;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Services.Balance;
using _Project.Scripts.Project.Services.Balance.Models;
using _Project.Scripts.Project.Services.StateMachines;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureSpawnService : IProjectService
    {
        void Spawn(string creatureId, bool enterOnSpawnState);
    }

    public class CreatureSpawnService : ICreatureSpawnService
    {
        [Inject] private IProjectBalanceService _balanceStorage;
        [Inject] private IGameSceneObjectPoolService _gameSceneObjectPoolService;
        [Inject] private IStateMachineCreator _stateMachineCreator;
        [Inject] private ICreatureModelCreator _modelCreator;
        [Inject] private ICreatureControllerRepository _controllerRepository;
        [Inject] private ICreatureSlotService _creatureSlotService;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void Spawn(string creatureId, bool enterOnSpawnState)
        {
            var creaturesBalanceStorage = _balanceStorage.Creatures;
            var tryGetResult = creaturesBalanceStorage.TryGetById(creatureId, out var creatureBalanceModel);

            if (!tryGetResult)
            {
                LogUtils.Error(this, $"Cant spawn creature with id [{creatureId}]");
                return;
            }

            var creatureType = creatureBalanceModel.CreatureType;

            switch (creatureType)
            {
                case CreatureType.PLAYER:
                    SpawnPlayer(creatureBalanceModel, enterOnSpawnState);
                    break;

                case CreatureType.ENEMY:
                    SpawnEnemy(creatureBalanceModel, enterOnSpawnState);
                    break;

                default:
                    LogUtils.Error(this, $"Spawn for creature type [{creatureType}] not implemented!");
                    break;
            }
        }

        private void SpawnPlayer(ICreatureBalanceModel balanceModel, bool enterOnSpawnState)
        {
            var creatureId = balanceModel.Id;

            var tryGetResult = TryGetFreeCreatureSlotController(true, out var creatureSlotController);

            if (!tryGetResult)
            {
                LogUtils.Error(this, $"Cant spawn creature, spawn point doesnt exist!");
                return;
            }

            var prefabId = balanceModel.PrefabId;

            var playerControllerPool = _gameSceneObjectPoolService.PlayerControllerPool;
            var playerController = playerControllerPool.Spawn();

            SetupCreatureModel(creatureId, playerController);
            SetupStateMachine(playerController);
            InitComponents(playerController);
            SetupCreaturePrefab(prefabId, playerController);
            InitStateMachineStateControllers(playerController);
            TryEnterOnSpawnState(enterOnSpawnState, playerController);

            creatureSlotController.SetCreatureController(playerController);
            _controllerRepository.Add(playerController);
        }

        private void SpawnEnemy(ICreatureBalanceModel balanceModel, bool enterOnSpawnState)
        { 
            var creatureId = balanceModel.Id;

            var tryGetResult = TryGetFreeCreatureSlotController(false, out var creatureSlotController);

            if (!tryGetResult)
            {
                LogUtils.Error(this, $"Cant spawn creature, spawn point doesnt exist!");
                return;
            }

            var prefabId = balanceModel.PrefabId;

            var enemyCreatureControllerPool = _gameSceneObjectPoolService.EnemyControllerPool;
            var enemyCreatureController = enemyCreatureControllerPool.Spawn();

            SetupCreatureModel(creatureId, enemyCreatureController);
            SetupStateMachine(enemyCreatureController);
            InitComponents(enemyCreatureController);
            SetupCreaturePrefab(prefabId, enemyCreatureController);
            InitStateMachineStateControllers(enemyCreatureController);
            TryEnterOnSpawnState(enterOnSpawnState, enemyCreatureController);

            creatureSlotController.SetCreatureController(enemyCreatureController);
            _controllerRepository.Add(enemyCreatureController);
        }

        bool TryGetFreeCreatureSlotController(bool playerSlot, out ICreatureSlotController creatureSlotController)
        {
            var result = false;
            creatureSlotController = null;

            if (playerSlot)
                result = _creatureSlotService.TryGetFreePlayerCreatureSlot(out creatureSlotController);
            else
                result = _creatureSlotService.TryGetFreeEnemyCreatureSlot(out creatureSlotController);

            return result;
        }

        void SetupCreatureModel(string creatureId, ICreatureController creatureController)
        {
            var creatureModel = _modelCreator.GetCreatureModel(creatureId);
            creatureController.SetupModel(creatureModel);
        }

        void SetupStateMachine(ICreatureController creatureController)
        {
            var stateMachineType = creatureController.CreatureModel.StateMachineType;
            var stateMachine = (ICreatureStateMachine)_stateMachineCreator.CreateStateMachine(stateMachineType);
            creatureController.SetupStateMachine(stateMachine);
        }

        void SetupCreaturePrefab(string prefabId, ICreatureController creatureController)
        {
            var prefabPool = _gameSceneObjectPoolService.CreaturePrefabPool;
            var creaturePrefab = prefabPool.Spawn(prefabId);
            creatureController.SetupPrefab(creaturePrefab);
        }

        void InitComponents(ICreatureController creatureController)
        {
            creatureController.InitComponents();
        }

        void InitStateMachineStateControllers(ICreatureController creatureController)
        {
            creatureController.CreatureStateMachine.InitStates();
        }

        void TryEnterOnSpawnState(bool enterOnSpawnState, ICreatureController creatureController)
        {
            if (!enterOnSpawnState)
                return;

            creatureController.CreatureStateMachine.EnterState(creatureController.OnSpawnState);
        }
    }
}