using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using IAwakable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async.IAwakable;
using IStartable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async.IStartable;
using IFlushable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync.IFlushable;
using _Project.Scripts.GameScene.Services.Creatures;

namespace _Project.Scripts.GameScene.GameLoop
{
    public interface IGameLoopController : IAwakable, IStartable, IFlushable, IMonoFixedUpdatable, IMonoUpdatable, IMonoLateUpdatable
    { 
    }

    public class GameLoopController : IGameLoopController
    {
        [Inject] private IMonoUpdater _monoUpdater;

        [Inject] private ICreatureService _creatureService;

        private bool _updateEnabled;

        public Task<bool> OnAwake()
        {
            _updateEnabled = false;
            return Task.FromResult(true);
        }

        public Task<bool> OnStart()
        {
            _updateEnabled = true;
            _monoUpdater.Subscribe((IMonoFixedUpdatable)this);
            _monoUpdater.Subscribe((IMonoUpdatable)this);
            _monoUpdater.Subscribe((IMonoLateUpdatable)this);
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            _updateEnabled = false;
            _monoUpdater.UnSubscribe((IMonoFixedUpdatable)this);
            _monoUpdater.UnSubscribe((IMonoUpdatable)this);
            _monoUpdater.UnSubscribe((IMonoLateUpdatable)this);
            return true;
        }

        public void OnFixedUpdate()
        {
            if (!_updateEnabled)
                return;

            _creatureService.OnFixedUpdate();
        }

        public void OnUpdate()
        {
            if (!_updateEnabled)
                return;

            _creatureService.OnUpdate();
        }

        public void OnLateUpdate()
        {
            if (!_updateEnabled)
                return;

            _creatureService.OnLateUpdate();
        }
    }
}