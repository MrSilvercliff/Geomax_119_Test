using System;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Time
{
    public interface ITimeService
    { 
        DateTime UtcNow { get; }
    }

    public class TimeService : ITimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}