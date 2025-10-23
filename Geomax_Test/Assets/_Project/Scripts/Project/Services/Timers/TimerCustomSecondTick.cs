using UnityEngine;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerCustomSecondTick : ITimerTick
    { 
    }

    public class TimerCustomSecondTick : TimerTick<TimerCustomSecondTick>, ITimerCustomSecondTick
    {
        public TimerCustomSecondTick(string id) : base(id)
        {
        }
    }
}