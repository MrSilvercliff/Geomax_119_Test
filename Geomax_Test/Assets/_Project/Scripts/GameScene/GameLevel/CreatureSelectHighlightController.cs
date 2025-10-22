using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.GameScene.Services.Combat;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Monobeh;
using UnityEngine;
using Zenject;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;
using IInitializable = ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync.IInitializable;

namespace _Project.Scripts.GameScene.GameLevel
{
    public class CreatureSelectHighlightController : ProjectMonoBehaviour, IInitializable, IMonoUpdatable
    {
        [Header("CREATURE SELECT HIGHLIGHT CONTROLLER")]
        [SerializeField] private CreatureType _targetCreatureType;
        [SerializeField] private SpriteRenderer _highlightSpriteRenderer;
        [SerializeField] private Color _playerCreatureHighlightColor;
        [SerializeField] private Color _enemyCreatureHighlightColor;

        [Inject] private ICombatService _combatService;

        private ICreatureController _currentHighlightedCreature;

        public bool Init()
        {
            _currentHighlightedCreature = null;
            InitColor();
            return true;
        }

        public void OnUpdate(float deltaTime)
        {
            var selectedCreature = _combatService.GetSelectedCreatureController(_targetCreatureType);

            if (selectedCreature == _currentHighlightedCreature)
                return;

            _currentHighlightedCreature = selectedCreature;

            if (_currentHighlightedCreature == null)
            {
                SetActive(false);
                return;
            }

            var isAlive = _currentHighlightedCreature.IsAlive();

            if (!isAlive)
            {
                SetActive(false);
                return;
            }

            SetActive(true);
            var position = _currentHighlightedCreature.Transform.position;
            position.z = 0;
            Transform.position = position;
        }

        private void InitColor()
        {
            switch (_targetCreatureType)
            {
                case CreatureType.PLAYER:
                    _highlightSpriteRenderer.color = _playerCreatureHighlightColor;
                    break;

                case CreatureType.ENEMY:
                    _highlightSpriteRenderer.color = _enemyCreatureHighlightColor;
                    break;
            }
        }
    }
}