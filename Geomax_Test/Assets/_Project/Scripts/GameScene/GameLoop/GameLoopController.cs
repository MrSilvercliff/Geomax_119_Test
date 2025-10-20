using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using IAwakable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async.IAwakable;
using IStartable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async.IStartable;
using IFlushable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync.IFlushable;
using _Project.Scripts.GameScene.Services.Creatures;
using _Project.Scripts.GameScene.Configs;

namespace _Project.Scripts.GameScene.GameLoop
{
    public interface IGameLoopController : IAwakable, IStartable, IFlushable, IMonoFixedUpdatable, IMonoUpdatable, IMonoLateUpdatable
    { 
    }

    public class GameLoopController : IGameLoopController
    {
        [Inject] private IMonoUpdater _monoUpdater;

        [Inject] private IGameStartConfig _gameStartConfig;
        [Inject] private ICreatureService _creatureService;

        private bool _updateEnabled;

        public Task<bool> OnAwake()
        {
            _updateEnabled = false;
            return Task.FromResult(true);
        }

        public Task<bool> OnStart()
        {
            OnStartSpawnCreatures();

            _monoUpdater.Subscribe((IMonoFixedUpdatable)this);
            _monoUpdater.Subscribe((IMonoUpdatable)this);
            _monoUpdater.Subscribe((IMonoLateUpdatable)this);
            _updateEnabled = true;
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

        private void OnStartSpawnCreatures()
        {
            var playerCreatureId = _gameStartConfig.PlayerCreatureId;
            _creatureService.SpawnCreature(playerCreatureId, true);

            var enemyCreatureIds = _gameStartConfig.EnemyCreatureIds;

            foreach (var creatureId in enemyCreatureIds)
                _creatureService.SpawnCreature(creatureId, true);
        }
    }
}