using Plugins.ZerglingUnityPlugins.Tools.Scripts.Repositories;
using System.Threading.Tasks;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.Project.Services.Timers
{
    public interface ITimerRepository : IRepositoryDictionary<string, ITimer>, IProjectService
    { 
    }

    public class TimerRepository : RepositoryDictionary<string, ITimer>, ITimerRepository
    {
        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        public override void Add(ITimer item)
        {
            var itemId = item.Id;
            Add(itemId, item);
        }

        public override void Add(string key, ITimer item)
        {
            _itemsDictionary.TryAdd(key, item);
        }

        public override void Remove(string key)
        {
            _itemsDictionary.Remove(key);
        }

        public override void Remove(ITimer item)
        {
            var itemId = item.Id;
            Remove(itemId);
        }
    }
}