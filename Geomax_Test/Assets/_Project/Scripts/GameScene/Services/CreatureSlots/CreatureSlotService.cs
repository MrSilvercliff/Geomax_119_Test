using _Project.Scripts.GameScene.CreatureSlot;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.ProjectService.AsyncSync;

namespace _Project.Scripts.GameScene.Services.CreatureSlots
{
    public interface ICreatureSlotService : IProjectService
    {
        void AddCreatureSlots(IReadOnlyCollection<ICreatureSlotController> playerCreatureSlots, IReadOnlyCollection<ICreatureSlotController> enemyCreatureSlots);
        bool TryGetFreePlayerCreatureSlot(out ICreatureSlotController slotController);
        bool TryGetFreeEnemyCreatureSlot(out ICreatureSlotController slotController);
    }

    public class CreatureSlotService : ICreatureSlotService
    {
        [Inject] private ICreatureSlotRepository _slotRepository;

        public async Task<bool> Init()
        {
            await _slotRepository.Init();
            return true;
        }

        public bool Flush()
        {
            _slotRepository.Flush();
            return true;
        }

        public void AddCreatureSlots(IReadOnlyCollection<ICreatureSlotController> playerCreatureSlots, IReadOnlyCollection<ICreatureSlotController> enemyCreatureSlots)
        {
            _slotRepository.AddPlayerCreatureSlots(playerCreatureSlots);
            _slotRepository.AddEnemyCreatureSlots(enemyCreatureSlots);
        }

        public bool TryGetFreePlayerCreatureSlot(out ICreatureSlotController slotController)
        {
            var result = _slotRepository.TryGetFreePlayerCreatureSlot(out slotController);
            return result;
        }

        public bool TryGetFreeEnemyCreatureSlot(out ICreatureSlotController slotController)
        {
            var result = _slotRepository.TryGetFreeEnemyCreatureSlot(out slotController);
            return result;
        }
    }
}