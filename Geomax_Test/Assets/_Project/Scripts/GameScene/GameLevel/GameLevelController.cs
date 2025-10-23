using _Project.Scripts.GameScene.CreatureSlot;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using IAwakable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async.IAwakable;
using IStartable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Async.IStartable;
using IFlushable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync.IFlushable;
using _Project.Scripts.GameScene.Configs;
using _Project.Scripts.GameScene.Services.Creatures;
using System.Threading.Tasks;
using Zenject;
using _Project.Scripts.GameScene.Services.CreatureSlots;
using _Project.Scripts.Project.Services.Timers;
using _Project.Scripts.GameScene.Services.Combat;
using _Project.Scripts.GameScene.Services.CreatureEffects;

namespace _Project.Scripts.GameScene.GameLevel
{
    public interface IGameLevelController : IAwakable, IStartable, IFlushable, IMonoFixedUpdatable, IMonoUpdatable, IMonoLateUpdatable
    {
    }

    public class GameLevelController : MonoBehaviour, IGameLevelController
    {
        [SerializeField] private CreatureSlotController[] _playerCreatureSlots;
        [SerializeField] private CreatureSlotController[] _enemyCreatureSlots;
        [SerializeField] private CreatureSelectHighlightController[] _creatureHighlightControllers;

        [Inject] private IMonoUpdater _monoUpdater;
        [Inject] private ITimerService _timerService;

        [Inject] private IGameStartConfig _gameStartConfig;
        [Inject] private ICreatureSlotService _creatureSlotService;
        [Inject] private ICreatureService _creatureService;
        [Inject] private ICombatService _combatService;
        [Inject] private ICreatureEffectService _creatureEffectService;

        private bool _updateEnabled;

        public Task<bool> OnAwake()
        {
            _updateEnabled = false;
            return Task.FromResult(true);
        }

        public async Task<bool> OnStart()
        {
            _creatureSlotService.AddCreatureSlots(_playerCreatureSlots, _enemyCreatureSlots);

            InitCreatureHighlights();
            OnStartSpawnCreatures();

            await _creatureService.OnLateStart();
            await _creatureEffectService.OnLateStart();

            SelectCreatureControllers();

            _monoUpdater.Subscribe((IMonoFixedUpdatable)this);
            _monoUpdater.Subscribe((IMonoUpdatable)this);
            _monoUpdater.Subscribe((IMonoLateUpdatable)this);
            _updateEnabled = true;
            return true;
        }

        public bool Flush()
        {
            _updateEnabled = false;
            _monoUpdater.UnSubscribe((IMonoFixedUpdatable)this);
            _monoUpdater.UnSubscribe((IMonoUpdatable)this);
            _monoUpdater.UnSubscribe((IMonoLateUpdatable)this);
            return true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (!_updateEnabled)
                return;

            _creatureService.OnFixedUpdate(deltaTime);
        }

        public void OnUpdate(float deltaTime)
        {
            if (!_updateEnabled)
                return;

            _creatureService.OnUpdate(deltaTime);

            foreach (var creatureHighlightController in _creatureHighlightControllers)
                creatureHighlightController.OnUpdate(deltaTime);
        }

        public void OnLateUpdate(float deltaTime)
        {
            if (!_updateEnabled)
                return;

            _creatureService.OnLateUpdate(deltaTime);
            _timerService.OnLateUpdate(deltaTime);
            _creatureEffectService.OnLateUpdate(deltaTime);
        }

        private void InitCreatureHighlights()
        {
            foreach (var creatureHighlightController in _creatureHighlightControllers)
                creatureHighlightController.Init();
        }

        private void OnStartSpawnCreatures()
        {
            var playerCreatureId = _gameStartConfig.PlayerCreatureId;
            _creatureService.SpawnCreature(playerCreatureId, false);

            var enemyCreatureIds = _gameStartConfig.EnemyCreatureIds;

            foreach (var creatureId in enemyCreatureIds)
                _creatureService.SpawnCreature(creatureId, false);
        }

        private void SelectCreatureControllers()
        {
            var playerCreatureController = _creatureService.PlayerController;
            _combatService.SelectCreatureController(playerCreatureController);
        }
    }
}