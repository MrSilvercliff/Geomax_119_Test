using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.Creatures;
using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Basis.Prefab;
using _Project.Scripts.GameScene.Creatures.Enemy;
using _Project.Scripts.GameScene.Creatures.Enemy.States;
using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.GameScene.Creatures.Player.States;
using _Project.Scripts.GameScene.GameLevel;
using _Project.Scripts.GameScene.ObjectPools;
using _Project.Scripts.GameScene.Services.Abilities;
using _Project.Scripts.GameScene.Services.Creatures;
using _Project.Scripts.GameScene.Services.CreatureSlots;
using _Project.Scripts.GameScene.Services.StateMachines;
using _Project.Scripts.Project.Services.Balance.Models;
using _Project.Scripts.Project.Services.StateMachines;
using UnityEngine;
using ZerglingUnityPlugins.ZenjectExtentions.SceneInstallers;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneInstaller : SceneInstaller
    {
        [SerializeField] private GameSceneObjectPoolContainers _objectPoolContainers;
        [SerializeField] private GameLevelController _levelController;

        [Header("CONFIGS")]
        [SerializeField] private CreaturePrefabConfig _creaturePrefabConfig;
        [SerializeField] private GameStartConfig _gameStartConfig;

        protected override void OnInstallBindings()
        {
            BindConfigs();

            BindAbilityServices();

            BindCreatureSlotServices();

            BindCreatureServices();

            BindObjectPools();

            BindStateMachineServices();

            BindGameLevelController();

            BindSceneServiceIniter();
        }

        private void BindSceneServiceIniter()
        {
            Container.Bind<IGameSceneServiceIniter>().To<GameSceneServiceIniter>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.Bind<ICreaturePrefabConfig>().FromInstance(_creaturePrefabConfig).AsSingle();
            Container.Bind<IGameStartConfig>().FromInstance(_gameStartConfig).AsSingle();
        }

        private void BindGameLevelController()
        { 
            Container.Bind<IGameLevelController>().FromInstance(_levelController).AsSingle();
        }

        private void BindAbilityServices()
        {
            BindAbilityFactories();

            Container.Bind<IAbilitiesProvider>().To<AbilitiesProvider>().AsSingle();
            Container.Bind<IAbilitiesCreator>().To<AbilitiesCreator>().AsSingle();
            Container.Bind<IAbilityService>().To<AbilityService>().AsSingle();
        }

        private void BindAbilityFactories()
        {
        }

        private void BindCreatureSlotServices()
        { 
            Container.Bind<ICreatureSlotRepository>().To<CreatureSlotRepository>().AsSingle();
            Container.Bind<ICreatureSlotService>().To<CreatureSlotService>().AsSingle();
        }

        private void BindCreatureServices()
        {
            BindCreatureStateFactories();

            Container.BindFactory<ICreatureBalanceModel, CreatureModel, CreatureModel.Factory>();

            Container.Bind<ICreatureModelCreator>().To<CreatureModelCreator>().AsSingle();

            Container.Bind<ICreatureSpawnService>().To<CreatureSpawnService>().AsSingle();

            Container.Bind<ICreatureControllerRepository>().To<CreatureControllerRepository>().AsSingle();
            Container.Bind<ICreatureControllerUpdater>().To<CreatureControllerUpdater>().AsSingle();
            Container.Bind<ICreatureService>().To<CreatureService>().AsSingle();
        }

        private void BindCreatureStateFactories()
        {
            BindPlayerStateFactories();
            BindEnemyCreatureStateFactories();
        }

        private void BindPlayerStateFactories()
        {
            Container.BindFactory<IPlayerController, PlayerStateStart, PlayerStateStart.Factory>();
            Container.BindFactory<IPlayerController, PlayerStateIdle, PlayerStateIdle.Factory>();
            Container.BindFactory<IPlayerController, PlayerStateAttack, PlayerStateAttack.Factory>();
            Container.BindFactory<IPlayerController, PlayerStateDeath, PlayerStateDeath.Factory>();

            Container.Bind<IPlayerStateCreator>().To<PlayerStateCreator>().AsSingle();
        }

        private void BindEnemyCreatureStateFactories()
        { 
            Container.BindFactory<IEnemyCreatureController, EnemyStateStart, EnemyStateStart.Factory>();
            Container.BindFactory<IEnemyCreatureController, EnemyStateIdle, EnemyStateIdle.Factory>();
            Container.BindFactory<IEnemyCreatureController, EnemyStateAttack, EnemyStateAttack.Factory>();
            Container.BindFactory<IEnemyCreatureController, EnemyStateHit, EnemyStateHit.Factory>();
            Container.BindFactory<IEnemyCreatureController, EnemyStateDeath, EnemyStateDeath.Factory>();
            Container.BindFactory<IEnemyCreatureController, EnemyStateDespawn, EnemyStateDespawn.Factory>();

            Container.Bind<IEnemyCreatureStateCreator>().To<EnemyCreatureStateCreator>().AsSingle();
        }

        private void BindObjectPools()
        {
            Container.Bind<IGameSceneObjectPoolContainers>().FromInstance(_objectPoolContainers).AsSingle();
            
            BindCreatureObjectPools();

            Container.Bind<IGameSceneObjectPoolService>().To<GameSceneObjectPoolService>().AsSingle();
        }

        private void BindCreatureObjectPools()
        {
            BindCreaturePrefabPool();
            BindPlayerControllerPool();
            BindEnemyCreatureControllerPool();
        }

        private void BindCreaturePrefabPool()
        {
            Container.BindFactory<CreaturePrefab, CreaturePrefab, CreaturePrefab.Factory>().FromFactory<CreaturePrefabFactory>();

            Container.Bind<ICreaturePrefabPool>().To<CreaturePrefabPool>().AsSingle();
        }

        private void BindPlayerControllerPool()
        {
            var poolItem = _objectPoolContainers.PlayerController;

            var prefab = poolItem.Prefab;
            var container = poolItem.Container;
            var poolInitSize = poolItem.PoolInitialSize;

            Container.BindMemoryPool<PlayerController, PlayerController.Pool>()
                .WithInitialSize(poolInitSize)
                .FromComponentInNewPrefab(prefab)
                .UnderTransform(container);
        }

        private void BindEnemyCreatureControllerPool()
        {
            var poolItem = _objectPoolContainers.EnemyCreatureController;

            var prefab = poolItem.Prefab;
            var container = poolItem.Container;
            var poolInitSize = poolItem.PoolInitialSize;

            Container.BindMemoryPool<EnemyCreatureController, EnemyCreatureController.Pool>()
                .WithInitialSize(poolInitSize)
                .FromComponentInNewPrefab(prefab)
                .UnderTransform(container);
        }

        private void BindStateMachineServices()
        {
            Container.BindFactory<PlayerStateMachine, PlayerStateMachine.Factory>();
            Container.BindFactory<EnemyCreatureStateMachine, EnemyCreatureStateMachine.Factory>();
            Container.Bind<IStateMachineCreator>().To<GameSceneStateMachineCreator>().AsSingle();
        }
    }
}