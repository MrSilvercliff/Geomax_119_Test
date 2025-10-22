using System;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerOneSecondTick : ITimer
    {
        event Action<ITimerOneSecondTick> TickEvent;
    }

    public class TimerOneSecondTick : Timer<TimerOneSecondTick>, ITimerOneSecondTick
    {
        public event Action<ITimerOneSecondTick> TickEvent;

        protected float _secondProgress;

        public TimerOneSecondTick(string id) : base(id)
        {
        }

        public override void OnProcess(float deltaTime)
        {
            base.OnProcess(deltaTime);

            if (Paused)
                return;

            if (Expired)
                return;

            _secondProgress += deltaTime;

            if (_secondProgress < 1)
                return;

            TickEvent?.Invoke(this);
            _secondProgress = 0f;
        }
    }
}
