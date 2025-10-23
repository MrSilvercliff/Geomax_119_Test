using System;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerTick : ITimer
    {
        event Action<ITimerTick> TickEvent;

        void SetTickTime(float time);
    }

    public abstract class TimerTick<T> : Timer<T>, ITimerTick where T : class, ITimerTick
    {
        public event Action<ITimerTick> TickEvent;

        protected float _tickProgress;
        protected float _tickTime;

        public TimerTick(string id) : base(id)
        {
        }

        public virtual void SetTickTime(float time)
        {
            _tickTime = time;
        }

        public override void OnProcess(float deltaTime)
        {
            base.OnProcess(deltaTime);

            if (Paused)
                return;

            if (Expired)
                return;

            _tickProgress += deltaTime;

            if (_tickProgress < _tickTime)
                return;

            TickEvent?.Invoke(this);
            _tickProgress = 0f;
        }

        public override void Reset()
        {
            base.Reset();
            _tickTime = 0f;
            _tickProgress = 0f;
        }
    }
}