using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerService : IProjectService, IMonoLateUpdatable
    { 
    }

    public class TimerService : ITimerService
    {
        [Inject] private ITimerIdProvider _idProvider;
        [Inject] private ITimerCreator _creator;
        [Inject] private ITimerRepository _repository;
        [Inject] private ITimerStartService _startService;
        [Inject] private ITimerUpdateService _updateService;

        public async Task<bool> Init()
        {
            await _idProvider.Init();
            await _creator.Init();
            await _repository.Init();
            await _startService.Init();
            await _updateService.Init();
            return true;
        }

        public bool Flush()
        {
            _idProvider.Flush();
            _creator.Flush();
            _repository.Flush();
            _startService.Flush();
            _updateService.Flush();
            return true;
        }

        public void OnLateUpdate(float deltaTime)
        {
            _updateService.OnLateUpdate(deltaTime);
        }
    }
}