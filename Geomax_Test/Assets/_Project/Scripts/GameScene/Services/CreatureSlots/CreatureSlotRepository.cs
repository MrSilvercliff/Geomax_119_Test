using _Project.Scripts.GameScene.Creatures.Basis;
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
        void AddPlayerCreatureSlots(IReadOnlyCollection<ICreatureSlotController> creatureSlots);
        void AddEnemyCreatureSlots(IReadOnlyCollection<ICreatureSlotController> creatureSlots);
        bool TryGetFreePlayerCreatureSlot(out ICreatureSlotController slotController);
        bool TryGetFreeEnemyCreatureSlot(out ICreatureSlotController slotController);
        bool TryGetSlotWithCreatureController(ICreatureController creatureController, out ICreatureSlotController slotController);
    }

    public class CreatureSlotRepository : ICreatureSlotRepository
    {
        private HashSet<ICreatureSlotController> _playerCreatureSlots;
        private HashSet<ICreatureSlotController> _enemyCreatureSlots;

        public CreatureSlotRepository()
        {
            _playerCreatureSlots = new();
            _enemyCreatureSlots = new();
        }

        public Task<bool> Init()
        {
            return Task.FromResult(true);
        }

        public bool Flush()
        {
            _playerCreatureSlots.Clear();
            _enemyCreatureSlots.Clear();
            return true;
        }

        public void AddPlayerCreatureSlots(IReadOnlyCollection<ICreatureSlotController> creatureSlots)
        {
            foreach (var creatureSlot in creatureSlots) 
                _playerCreatureSlots.Add(creatureSlot);
        }

        public void AddEnemyCreatureSlots(IReadOnlyCollection<ICreatureSlotController> creatureSlots)
        {
            foreach (var creatureSlot in creatureSlots)
                _enemyCreatureSlots.Add(creatureSlot);
        }

        public bool TryGetFreePlayerCreatureSlot(out ICreatureSlotController slotController)
        {
            var result = TryGetFreeCreatureSlot(_playerCreatureSlots, out slotController);
            return result;
        }

        public bool TryGetFreeEnemyCreatureSlot(out ICreatureSlotController slotController)
        {
            var result = TryGetFreeCreatureSlot(_enemyCreatureSlots, out slotController);
            return result;
        }

        private bool TryGetFreeCreatureSlot(HashSet<ICreatureSlotController> creatureSlotSet, out ICreatureSlotController slotController)
        {
            foreach (var creatureSlotController in creatureSlotSet)
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

        public bool TryGetSlotWithCreatureController(ICreatureController creatureController, out ICreatureSlotController slotController)
        {
            slotController = null;

            foreach (var playerCreatureSlot in _playerCreatureSlots)
            {
                if (playerCreatureSlot.Contains(creatureController))
                {
                    slotController = playerCreatureSlot;
                    break;
                }
            }

            if (slotController != null)
                return true;

            foreach (var enemyCreatureSlot in _enemyCreatureSlots)
            {
                if (enemyCreatureSlot.Contains(creatureController))
                {
                    slotController = enemyCreatureSlot;
                    break;
                }
            }

            var result = slotController != null;
            return result;
        }
    }

}