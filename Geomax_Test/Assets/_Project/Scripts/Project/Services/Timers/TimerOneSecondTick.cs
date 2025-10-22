using System;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerOneSecondTick : ITimerTick
    {
    }

    public class TimerOneSecondTick : TimerTick<TimerOneSecondTick>, ITimerOneSecondTick
    {
        public TimerOneSecondTick(string id) : base(id)
        {
            _tickTime = 1f;
        }

        public override void SetTickTime(float time)
        {
            Debug.LogError($"DONT USE SET TICK TIME! USE TIMER TICK CLASS INSTED!");
        }
    }
}
