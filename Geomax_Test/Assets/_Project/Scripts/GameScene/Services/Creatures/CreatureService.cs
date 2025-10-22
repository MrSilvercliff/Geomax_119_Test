using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.Creatures.Basis;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureService : IProjectService, ILateStartable, IMonoFixedUpdatable, IMonoUpdatable, IMonoLateUpdatable
    {
        ICreatureController PlayerController { get; }

        void SpawnCreature(string creatureId, bool enterOnSpawnState);
        void DespawnCreature(ICreatureController creatureController);

        ICreatureController GetAttackTargetForPlayerCreature();
        ICreatureController GetAttackTargetForEnemyCreature();
    }

    public class CreatureService : ICreatureService
    {
        public ICreatureController PlayerController => _creatureControllerRepository.PlayerController;

        [Inject] private ICreaturePrefabConfig _creaturePrefabConfig;

        [Inject] private ICreatureControllerRepository _creatureControllerRepository;
        [Inject] private ICreatureControllerUpdater _creatureControllerUpdater;
        [Inject] private ICreatureModelCreator _creatureModelCreator;
        [Inject] private ICreatureSpawnService _creatureSpawnService;
        [Inject] private ICreatureDespawnService _creatureDespawnService;
        [Inject] private ICreatureAttackTargetProvider _creatureAttackTargetProvider;

        public async Task<bool> Init()
        {
            _creaturePrefabConfig.Init();

            await _creatureControllerRepository.Init();
            await _creatureControllerUpdater.Init();
            await _creatureModelCreator.Init();
            await _creatureSpawnService.Init();
            await _creatureDespawnService.Init();
            await _creatureAttackTargetProvider.Init();
            return true;
        }

        public bool Flush()
        {
            _creatureControllerRepository.Flush();
            _creatureControllerUpdater.Flush();
            _creatureModelCreator.Flush();
            _creatureSpawnService.Flush();
            _creatureDespawnService.Flush();
            _creatureAttackTargetProvider.Flush();
            return true;
        }

        public async Task<bool> OnLateStart()
        {
            _creatureControllerRepository.OnLateUpdate(0);
            await _creatureControllerUpdater.OnLateStart();
            return true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            _creatureControllerUpdater.OnFixedUpdate(deltaTime);
        }

        public void OnUpdate(float deltaTime)
        {
            _creatureControllerUpdater.OnUpdate(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            _creatureControllerUpdater.OnLateUpdate(deltaTime);
            _creatureControllerRepository.OnLateUpdate(deltaTime);
        }

        public void SpawnCreature(string creatureId, bool enterOnSpawnState)
        {
            _creatureSpawnService.Spawn(creatureId, enterOnSpawnState);
        }

        public void DespawnCreature(ICreatureController creatureController)
        {
            _creatureDespawnService.Despawn(creatureController);
        }

        public ICreatureController GetAttackTargetForPlayerCreature()
        {
            var result = _creatureAttackTargetProvider.GetAttackTargetForPlayerCreature();
            return result;
        }

        public ICreatureController GetAttackTargetForEnemyCreature()
        {
            var result = _creatureAttackTargetProvider.GetAttackTargetForEnemyCreature();
            return result;
        }
    }
}