using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.CreatureSlots
{
    public interface ICreatureSlotService : IProjectService
    { 
    }

    public class CreatureSlotService : ICreatureSlotService
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }
    }
}