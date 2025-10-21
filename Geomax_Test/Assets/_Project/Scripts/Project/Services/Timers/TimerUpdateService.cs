using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerUpdateService : IProjectService, IMonoLateUpdatable
    { 
    }

    public class TimerUpdateService : ITimerUpdateService
    {
        [Inject] private ITimerRepository _repository;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public void OnLateUpdate(float deltaTime)
        {
            var allTimers = _repository.GetAll();

            foreach (var timer in allTimers)
                timer.OnProcess(deltaTime);
        }
    }
}