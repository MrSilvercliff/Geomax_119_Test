using System;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerSecondTick : ITimer
    {
        event Action<ITimerSecondTick> TickEvent;
    }

    public class TimerSecondTick : Timer<TimerSecondTick>, ITimerSecondTick
    {
        public event Action<ITimerSecondTick> TickEvent;

        protected float _secondProgress;

        public TimerSecondTick(string id) : base(id)
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
