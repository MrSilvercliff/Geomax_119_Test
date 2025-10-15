using _Project.Scripts.GameScene.Abilities;
using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.Creatures;
using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Creatures.Basis.Prefab;
using _Project.Scripts.GameScene.Creatures.Player;
using _Project.Scripts.GameScene.Creatures.Player.States;
using _Project.Scripts.GameScene.Services.Abilities;
using _Project.Scripts.GameScene.Services.Creatures;
using _Project.Scripts.GameScene.Services.StateMachines;
using _Project.Scripts.Project.Services.Balance.Models;
using _Project.Scripts.Project.Services.StateMachines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.ZenjectExtentions.SceneInstallers;

namespace _Project.Scripts.GameScene.Scene
{
    public class GameSceneInstaller : SceneInstaller
    {
        [SerializeField] private GameSceneObjectPoolContainers _objectPoolContainers;

        [Header("CONFIGS")] 
        [SerializeField] private CreaturePrefabConfig _creaturePrefabConfig;

        protected override void OnInstallBindings()
        {
            BindConfigs();

            BindAbilityServices();

            BindCreatureServices();

            BindSpawnPointsServices();

            BindObjectPools();

            BindStateMachineServices();

            BindSceneServiceIniter();
        }

        private void BindSceneServiceIniter()
        {
            Container.Bind<IGameSceneServiceIniter>().To<GameSceneServiceIniter>().AsSingle();
        }

        private void BindConfigs()
        {
            Container.Bind<ICreaturePrefabConfig>().FromInstance(_creaturePrefabConfig).AsSingle();
        }

        private void BindAbilityServices()
        {
            BindAbilityFactories();

            Container.Bind<IAbilitiesProvider>().To<AbilitiesProvider>().AsSingle();
        }

        private void BindAbilityFactories()
        {
        }

        private void BindCreatureServices()
        {
            BindCreatureStateFactories();

            Container.Bind<ICreatureHelper>().To<CreatureHelper>().AsSingle();

            Container.BindFactory<ICreatureBalanceModel, CreatureModel, CreatureModel.Factory>();

            Container.Bind<ICreatureModelCreator>().To<CreatureModelCreator>().AsSingle();

            Container.Bind<ICreatureSpawnController>().To<CreatureSpawnController>().AsSingle();

            Container.Bind<ICreatureControllerRepository>().To<CreatureControllerRepository>().AsSingle();
            Container.Bind<ICreatureControllerUpdater>().To<CreatureControllerUpdater>().AsSingle();
            Container.Bind<ICreatureService>().To<CreatureService>().AsSingle();
        }

        private void BindCreatureStateFactories()
        {
            BindPlayerStateFactories();
        }

        private void BindPlayerStateFactories()
        {
            Container.Bind<IPlayerStateCreator>().To<PlayerStateCreator>().AsSingle();
        }

        private void BindSpawnPointsServices()
        {
        }

        private void BindObjectPools()
        {
            Container.Bind<IGameSceneObjectPoolContainers>().FromInstance(_objectPoolContainers).AsSingle();
            
            BindCreatureObjectPools();
        }

        private void BindCreatureObjectPools()
        {
            BindPlayerControllerPool();
            BindCreaturePrefabPool();
        }

        private void BindPlayerControllerPool()
        {
            var poolItem = _objectPoolContainers.PlayerController;

            var prefab = poolItem.Prefab;
            var container = poolItem.Container;

            Container.BindMemoryPool<PlayerController, PlayerControllerPool>()
                .WithInitialSize(1)
                .FromComponentInNewPrefab(prefab)
                .UnderTransform(container);
        }

        private void BindCreaturePrefabPool()
        {
            Container.BindFactory<CreaturePrefab, CreaturePrefab, CreaturePrefab.Factory>().FromFactory<CreaturePrefabFactory>();

            Container.Bind<ICreaturePrefabPool>().To<CreaturePrefabPool>().AsSingle();
        }

        private void BindStateMachineServices()
        {
            Container.BindFactory<PlayerStateMachine, PlayerStateMachine.Factory>();
            Container.Bind<IStateMachineCreator>().To<GameSceneStateMachineCreator>().AsSingle();
        }
    }
}