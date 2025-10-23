using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerCustomSecondTickInfinite : ITimerTick
    { 
    }

    public class TimerCustomSecondTickInfinite : TimerTick<TimerCustomSecondTickInfinite>, ITimerCustomSecondTickInfinite
    {
        public TimerCustomSecondTickInfinite(string id) : base(id)
        {
        }

        public override void OnProcess(float deltaTime)
        {
            base.OnProcess(deltaTime);
            SetTimeSpend(0);
        }
    }
}