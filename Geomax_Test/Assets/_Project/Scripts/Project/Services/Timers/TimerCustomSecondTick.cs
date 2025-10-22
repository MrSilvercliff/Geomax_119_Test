using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerCustomSecondTick : ITimerTick
    { 
    }

    public class TimerCustomSecondTick : TimerTick<TimerCustomSecondTick>
    {
        public TimerCustomSecondTick(string id) : base(id)
        {
        }
    }
}