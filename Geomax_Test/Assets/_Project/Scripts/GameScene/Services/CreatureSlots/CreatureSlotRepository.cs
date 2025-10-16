using _Project.Scripts.GameScene.CreatureSlot;
using _Project.Scripts.GameScene.GameLevel;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;
using ZerglingUnityPlugins.Tools.Scripts.Log;

namespace _Project.Scripts.GameScene.Services.CreatureSlots
{
    public interface ICreatureSlotRepository : IProjectService
    {
        bool TryGetFreePlayerCreatureSlot(out ICreatureSlotController slotController);
        bool TryGetFreeEnemyCreatureSlot(out ICreatureSlotController slotController);
    }

    public class CreatureSlotRepository : ICreatureSlotRepository
    {
        private Dictionary<int, ICreatureSlotController> _playerCreatureSlots;
        private Dictionary<int, ICreatureSlotController> _enemyCreatureSlots;

        [Inject] IGameLevelController _gameLevelController;

        public CreatureSlotRepository()
        {
            _playerCreatureSlots = new();
            _enemyCreatureSlots = new();
        }

        public Task<bool> Init()
        {
            InitSlotDictionary(_playerCreatureSlots, _gameLevelController.PlayerCreatureSlots);
            InitSlotDictionary(_enemyCreatureSlots, _gameLevelController.EnemyCreatureSlots);
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            return true;
        }

        private void InitSlotDictionary(Dictionary<int, ICreatureSlotController> dictionary, IReadOnlyCollection<ICreatureSlotController> creatureSlotControllers)
        {
            foreach (var creatureSlotController in creatureSlotControllers)
            {
                var slotIndex = creatureSlotController.SlotIndex;

                if (dictionary.ContainsKey(slotIndex))
                {
                    LogUtils.Error(this, $"Creature slot controller with index [{slotIndex}] ALREADY EXIST!");
                    continue;
                }

                dictionary[slotIndex] = creatureSlotController;
            }
        }

        public bool TryGetFreePlayerCreatureSlot(out ICreatureSlotController slotController)
        {
            foreach (var creatureSlotController in _playerCreatureSlots.Values)
            {
                var slotIsEmpty = creatureSlotController.IsEmpty();

                if (slotIsEmpty)
                {
                    slotController = creatureSlotController;
                    return true;
                }
            }

            slotController = null;
            return false;
        }

        public bool TryGetFreeEnemyCreatureSlot(out ICreatureSlotController slotController)
        {
            foreach (var creatureSlotController in _enemyCreatureSlots.Values)
            {
                var slotIsEmpty = creatureSlotController.IsEmpty();

                if (slotIsEmpty)
                {
                    slotController = creatureSlotController;
                    return true;
                }
            }

            slotController = null;
            return false;
        }
    }
}