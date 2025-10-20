using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureService : IProjectService, IMonoFixedUpdatable, IMonoUpdatable, IMonoLateUpdatable
    {
        void SpawnCreature(string creatureId, bool enterOnSpawnState);
    }

    public class CreatureService : ICreatureService
    {
        [Inject] private ICreatureControllerRepository _creatureControllerRepository;
        [Inject] private ICreatureControllerUpdater _creatureControllerUpdater;
        [Inject] private ICreatureModelCreator _creatureModelCreator;
        [Inject] private ICreatureSpawnService _creatureSpawnController;

        public async Task<bool> Init()
        {
            await _creatureControllerRepository.Init();
            await _creatureControllerUpdater.Init();
            await _creatureModelCreator.Init();
            await _creatureSpawnController.Init();
            return true;
        }

        public bool Flush()
        {
            _creatureControllerRepository.Flush();
            _creatureControllerUpdater.Flush();
            _creatureModelCreator.Flush();
            _creatureSpawnController.Flush();
            return true;
        }

        public void OnFixedUpdate()
        {
            _creatureControllerUpdater.OnFixedUpdate();
        }

        public void OnUpdate()
        {
            _creatureControllerUpdater.OnUpdate();
        }

        public void OnLateUpdate()
        {
            _creatureControllerUpdater.OnLateUpdate();
            _creatureControllerRepository.OnLateUpdate();
        }

        public void SpawnCreature(string creatureId, bool enterOnSpawnState)
        {
            _creatureSpawnController.Spawn(creatureId, enterOnSpawnState);
        }
    }
}