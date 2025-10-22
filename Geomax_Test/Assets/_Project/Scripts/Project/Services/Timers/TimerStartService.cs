using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerStartService : IProjectService
    {
        ITimer StartTimer(string id, float duration);
        ITimerOneSecondTick StartTimerSecondTick(string id, float duration);
        void PauseTimer(string timerId, bool paused);
        void StopTimer(string timerId, bool reset);
    }

    public class TimerStartService : ITimerStartService
    {
        [Inject] private ITimerRepository _repository;
        [Inject] private ITimerCreator _creator;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public ITimer StartTimer(string id, float duration)
        {
            ITimer timer = null;

            var tryResult = _repository.TryGet(id, out timer);

            if (!tryResult)
            {
                timer = _creator.Create(id);
                _repository.Add(id, timer);
            }

            timer.Start(duration);
            return timer;
        }

        public ITimerOneSecondTick StartTimerSecondTick(string id, float duration)
        {
            ITimerOneSecondTick timer = null;

            var tryResult = _repository.TryGet(id, out var repositoryTimer);

            if (tryResult)
                timer = (ITimerOneSecondTick)repositoryTimer;
            else
            {
                timer = _creator.CreateSecondTick(id);
                _repository.Add(id, timer);
            }

            timer.Start(duration);
            return timer;
        }

        public void PauseTimer(string timerId, bool paused)
        {
            var tryResult = _repository.TryGet(timerId, out var timer);

            if (!tryResult)
                return;

            timer.Pause(paused);
        }

        public void StopTimer(string timerId, bool reset)
        {
            var tryResult = _repository.TryGet(timerId, out var timer);

            if (!tryResult)
                return;

            timer.Stop(reset);
        }
    }
}