using _Project.Scripts.GameScene.CreatureSlot;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameScene.GameLevel
{
    public interface IGameLevelController
    {
        IReadOnlyCollection<ICreatureSlotController> PlayerCreatureSlots { get; }
        IReadOnlyCollection<ICreatureSlotController> EnemyCreatureSlots { get; }
    }

    public class GameLevelController : MonoBehaviour, IGameLevelController
    {
        public IReadOnlyCollection<ICreatureSlotController> PlayerCreatureSlots => _playerCreatureSlots;
        public IReadOnlyCollection<ICreatureSlotController> EnemyCreatureSlots => _enemyCreatureSlots;

        [SerializeField] private CreatureSlotController[] _playerCreatureSlots;
        [SerializeField] private CreatureSlotController[] _enemyCreatureSlots;
    }
}