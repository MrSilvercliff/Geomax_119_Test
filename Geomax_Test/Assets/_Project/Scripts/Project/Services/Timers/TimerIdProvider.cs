using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerIdProvider : IProjectService
    {
        string GetCreatureAbilityCooldownTimerId(int creatureControllerInstanceId, string abilityId);
    }

    public class TimerIdProvider : ITimerIdProvider
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public string GetCreatureAbilityCooldownTimerId(int creatureControllerInstanceId, string abilityId)
        {
            var result = $"{creatureControllerInstanceId}_{abilityId}";
            return result;
        }
    }
}