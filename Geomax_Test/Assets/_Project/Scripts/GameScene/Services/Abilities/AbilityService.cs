using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.Abilities
{
    public interface IAbilityService : IProjectService
    { 
    }

    public class AbilityService : IAbilityService
    {
        [Inject] private IAbilitiesCreator _abilitiesCreator;
        [Inject] private IAbilitiesProvider _abilitiesProvider;

        public async Task<bool> Init()
        {
            await _abilitiesCreator.Init();
            await _abilitiesProvider.Init();
            return true;
        }

        public bool Flush()
        {
            _abilitiesCreator.Flush();
            _abilitiesProvider.Flush();
            return true;
        }
    }
}