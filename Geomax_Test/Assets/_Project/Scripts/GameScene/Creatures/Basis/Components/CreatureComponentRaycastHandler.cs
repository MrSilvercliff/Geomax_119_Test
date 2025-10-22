using _Project.Scripts.GameScene.Services.Combat;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.GameScene.Creatures.Basis.Components
{
    public interface ICreatureComponentRaycastHandler : ICreatureComponentBase, IPointerDownHandler
    { 
    }

    public class CreatureComponentRaycastHandler : CreatureComponentBase, ICreatureComponentRaycastHandler
    {
        [Inject] private ICombatService _combatService;

        private ICreatureController _creatureController;

        public override void Init(ICreatureComponentContainer componentContainer, ICreatureController creatureController)
        {
            _creatureController = creatureController;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_creatureController == null)
                return;

            var isAlive = _creatureController.IsAlive();

            if (!isAlive)
                return;

            _combatService.SelectCreatureController(_creatureController);
        }
    }
}