using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerCreator : IProjectService
    {
        ITimer Create(string id);
        ITimerSecondTick CreateSecondTick(string id);
    }

    public class TimerCreator : ITimerCreator
    {
        [Inject] private Timer.Factory _timerFactory;
        [Inject] private TimerSecondTick.Factory _timerSecondTickFactory;

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public ITimer Create(string id)
        {
            var result = _timerFactory.Create(id);
            return result;
        }

        public ITimerSecondTick CreateSecondTick(string id)
        {
            var result = _timerSecondTickFactory.Create(id);
            return result;
        }
    }
}