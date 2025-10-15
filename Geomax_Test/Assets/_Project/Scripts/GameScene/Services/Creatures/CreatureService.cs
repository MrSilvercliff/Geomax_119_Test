using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Creatures
{
    public interface ICreatureService : IProjectService
    { 
    }

    public class CreatureService : ICreatureService
    {
        [Inject] private ICreatureControllerRepository _creatureControllerRepository;
        [Inject] private ICreatureControllerUpdater _creatureControllerUpdater;
        [Inject] private ICreatureModelCreator _creatureModelCreator;
        [Inject] private ICreatureSpawnController _creatureSpawnController;

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
    }
}