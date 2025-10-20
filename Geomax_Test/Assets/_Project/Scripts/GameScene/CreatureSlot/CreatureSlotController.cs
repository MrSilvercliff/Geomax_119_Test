using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Extensions;
using _Project.Scripts.Project.Monobeh;
using UnityEngine;

namespace _Project.Scripts.GameScene.CreatureSlot
{
    public interface ICreatureSlotController : IProjectMonoBehaviour
    {
        int SlotIndex { get; }

        bool IsEmpty();
        void SetCreatureController(ICreatureController creatureController);
    }

    public class CreatureSlotController : ProjectMonoBehaviour, ICreatureSlotController
    {
        public int SlotIndex => _slotIndex;

        [Header("CREATURE SLOT CONTROLLER")]
        [SerializeField] private int _slotIndex;
        [SerializeField] private GameObject _emptyState;
        [SerializeField] private Transform _creatureContainer;

        private ICreatureController _creatureController;

        protected override void OnAwake()
        {
            _creatureController = null;
        }

        public bool IsEmpty()
        {
            var result = _creatureController == null;
            return result;
        }

        public void SetCreatureController(ICreatureController creatureController)
        {
            _emptyState.SetActive(false);

            _creatureController = creatureController;

            if (_creatureContainer == null)
                return;

            _creatureController.Transform.SetParent(_creatureContainer);
            _creatureController.Transform.ResetLocalPosition();
            _creatureController.Transform.ResetLocalRotation();
            _creatureController.Transform.ResetLocalScale();
        }
    }
}