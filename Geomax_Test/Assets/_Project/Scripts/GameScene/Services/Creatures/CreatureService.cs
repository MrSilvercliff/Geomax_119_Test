using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.Creatures.Basis;
using System.Collections.Generic;
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
        IReadOnlyCollection<ICreatureController> GetAllCreatureControllers();
        bool TryGetCreatureControllerByInstanceId(int instanceId, out ICreatureController creatureController);

        void SpawnCreature(string creatureId, bool enterOnSpawnState);
        void DespawnCreature(ICreatureController creatureController);
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

        public async Task<bool> Init()
        {
            _creaturePrefabConfig.Init();

            await _creatureControllerRepository.Init();
            await _creatureControllerUpdater.Init();
            await _creatureModelCreator.Init();
            await _creatureSpawnService.Init();
            await _creatureDespawnService.Init();
            return true;
        }

        public bool Flush()
        {
            _creatureControllerRepository.Flush();
            _creatureControllerUpdater.Flush();
            _creatureModelCreator.Flush();
            _creatureSpawnService.Flush();
            _creatureDespawnService.Flush();
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

        public IReadOnlyCollection<ICreatureController> GetAllCreatureControllers()
        {
            var result = _creatureControllerRepository.GetAll();
            return result;
        }

        public bool TryGetCreatureControllerByInstanceId(int instanceId, out ICreatureController creatureController)
        {
            var result = _creatureControllerRepository.TryGetByInstanceId(instanceId, out creatureController);
            return result;
        }

        public void SpawnCreature(string creatureId, bool enterOnSpawnState)
        {
            _creatureSpawnService.Spawn(creatureId, enterOnSpawnState);
        }

        public void DespawnCreature(ICreatureController creatureController)
        {
            _creatureDespawnService.Despawn(creatureController);
        }
    }
}