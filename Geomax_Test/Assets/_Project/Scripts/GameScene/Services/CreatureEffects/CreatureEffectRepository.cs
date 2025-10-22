using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Effects.CreatureEffects;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Services.CreatureEffects
{
    public interface ICreatureEffectRepository : IProjectService, IMonoLateUpdatable
    { 
        bool TryGetCreatureEffects(int creatureControllerInstanceId, out IReadOnlyList<ICreatureEffectController> creatureEffectControllers);
        bool TryGetCreatureControllerInstanceId(ICreatureEffectController creatureEffectController, out int creatureControllerInstanceId);

        void Add(int creatureControllerInstanceId, ICreatureEffectController creatureEffectController);
        void Remove(ICreatureEffectController creatureEffectController);

        void Clear();
    }

    public class CreatureEffectRepository : ICreatureEffectRepository
    {
        [Inject] private CreatureEffectController.Pool _pool;

        private Dictionary<int, List<ICreatureEffectController>> _activeEffectsByCreatureControllerInstanceId;
        private Dictionary<ICreatureEffectController, int> _creatureControllerInstanceIdByEffect;

        private HashSet<CreatureEffectRepositoryTempItem> _toAddCreatureEffects;
        private HashSet<CreatureEffectRepositoryTempItem> _toRemoveCreatureEffects;

        public CreatureEffectRepository()
        {
            _activeEffectsByCreatureControllerInstanceId = new();
            _creatureControllerInstanceIdByEffect = new();

            _toAddCreatureEffects = new();
            _toRemoveCreatureEffects = new();
        }

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            Clear();
            return true;
        }

        public bool TryGetCreatureEffects(int creatureControllerInstanceId, out IReadOnlyList<ICreatureEffectController> creatureEffectControllers)
        {
            var result = _activeEffectsByCreatureControllerInstanceId.TryGetValue(creatureControllerInstanceId, out var list);
            creatureEffectControllers = list;
            return result;
        }

        public bool TryGetCreatureControllerInstanceId(ICreatureEffectController creatureEffectController, out int creatureControllerInstanceId)
        {
            var result = _creatureControllerInstanceIdByEffect.TryGetValue(creatureEffectController, out creatureControllerInstanceId);
            return result;
        }

        public void Add(int creatureControllerInstanceId, ICreatureEffectController creatureEffectController)
        {
            var tempItem = new CreatureEffectRepositoryTempItem(creatureControllerInstanceId, creatureEffectController);
            _toAddCreatureEffects.Add(tempItem);
        }

        public void Remove(ICreatureEffectController creatureEffectController)
        {
            var creatureControllerInstanceId = _creatureControllerInstanceIdByEffect[creatureEffectController];
            var tempItem = new CreatureEffectRepositoryTempItem(creatureControllerInstanceId, creatureEffectController);
            _toRemoveCreatureEffects.Add(tempItem);
        }

        public void Clear()
        {
            foreach (var list in _activeEffectsByCreatureControllerInstanceId.Values)
            {
                foreach (var creatureEffectController in list)
                    _pool.Despawn((CreatureEffectController)creatureEffectController);

                list.Clear();
            }

            _activeEffectsByCreatureControllerInstanceId.Clear();
            _creatureControllerInstanceIdByEffect.Clear();
            _toAddCreatureEffects.Clear();
            _toRemoveCreatureEffects.Clear();
        }

        public void OnLateUpdate(float deltaTime)
        {
            CheckToRemoveCreatureEffectControllers();
            CheckToAddCreatureEffectControllers();
        }

        private void CheckToAddCreatureEffectControllers()
        {
            foreach (var tempItem in _toAddCreatureEffects)
            {
                var creatureControllerInstanceId = tempItem.CreatureControllerInstanceId;
                var creatureEffectController = tempItem.CreatureEffectController;

                if (!_activeEffectsByCreatureControllerInstanceId.TryGetValue(creatureControllerInstanceId, out var list))
                {
                    list = new();
                    _activeEffectsByCreatureControllerInstanceId[creatureControllerInstanceId] = list;
                }

                list.Add(creatureEffectController);
                _creatureControllerInstanceIdByEffect[creatureEffectController] = creatureControllerInstanceId;
            }
        }

        private void CheckToRemoveCreatureEffectControllers()
        {
            foreach (var tempItem in _toRemoveCreatureEffects)
            {
                var creatureControllerInstanceId = tempItem.CreatureControllerInstanceId;
                var creatureEffectController = tempItem.CreatureEffectController;

                _creatureControllerInstanceIdByEffect.Remove(creatureEffectController);

                if (!_activeEffectsByCreatureControllerInstanceId.TryGetValue(creatureControllerInstanceId, out var list))
                { 
                    list = new();
                    _activeEffectsByCreatureControllerInstanceId[(creatureControllerInstanceId)] = list;
                    continue;
                }

                list.Remove(creatureEffectController);
                _pool.Despawn((CreatureEffectController)creatureEffectController);
            }
        }

        private struct CreatureEffectRepositoryTempItem
        {
            public int CreatureControllerInstanceId { get; private set; }
            public ICreatureEffectController CreatureEffectController { get; private set; }

            public CreatureEffectRepositoryTempItem(int creatureControllerInstanceId, ICreatureEffectController creatureEffectController)
            {
                CreatureControllerInstanceId = creatureControllerInstanceId;
                CreatureEffectController = creatureEffectController;
            }
        }
    }
}