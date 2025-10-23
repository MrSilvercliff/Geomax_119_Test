using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Enums;
using _Project.Scripts.Project.Monobeh;
using _Project.Scripts.Project.ObjectPools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.UI.Views.Main
{
    public class CreatureResourceWidget : ProjectMonoBehaviour, IProjectPoolable, IMonoUpdatable
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _textMaxValue;
        [SerializeField] private TMP_Text _textCurrentValue;
        [SerializeField] private Color _hitPointsColor;
        [SerializeField] private Color _manaPointsColor;

        private ICreatureResourceValue _creatureResourceValue;

        public void Setup(ICreatureResourceValue creatureResourceValue)
        {
            _creatureResourceValue = creatureResourceValue;
            RefreshColor();
            _textMaxValue.text = _creatureResourceValue.MaxValue.ToString();
        }

        private void RefreshColor()
        {
            switch (_creatureResourceValue.ResourceType)
            {
                case CreatureResourceType.HIT_POINTS:
                    _fillImage.color = _hitPointsColor;
                    break;

                case CreatureResourceType.MANA_POINTS:
                    _fillImage.color= _manaPointsColor;
                    break;
            }
        }

        public void OnUpdate(float deltaTime)
        {
            if (_creatureResourceValue == null)
                return;

            var currentValue = _creatureResourceValue.CurrentValue;
            var maxValue = _creatureResourceValue.MaxValue;
            var fillAmount = (float)currentValue / maxValue;
            _fillImage.fillAmount = fillAmount;
            _textCurrentValue.text = currentValue.ToString();
        }

        public void OnCreated()
        {
            _creatureResourceValue = null;
        }

        public void OnSpawned()
        {
            SetActive(false);
        }

        public void OnDespawned()
        {
            _creatureResourceValue = null;
        }

        public class Pool : ProjectMonoMemoryPool<CreatureResourceWidget> { }
    }
}