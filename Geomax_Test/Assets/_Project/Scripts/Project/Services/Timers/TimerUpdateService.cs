using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerUpdateService : IMonoLateUpdatable
    { 
    }

    public class TimerUpdateService : ITimerUpdateService
    {
        [Inject] private ITimerRepository _repository;

        public void OnLateUpdate(float deltaTime)
        {
            var allTimers = _repository.GetAll();

            foreach (var timer in allTimers)
                timer.OnProcess(deltaTime);
        }
    }
}