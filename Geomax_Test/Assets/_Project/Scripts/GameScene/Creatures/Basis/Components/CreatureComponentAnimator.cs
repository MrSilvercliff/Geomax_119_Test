using _Project.Scripts.GameScene.Animations;
using _Project.Scripts.Project.Animations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.GameScene.Creatures.Basis.Components
{
    public interface ICreatureComponentAnimator : ICreatureComponentBase
    { 
        MonoBehaviourAnimatorController AnimatorController { get; }
    }

    public class CreatureComponentAnimator : CreatureComponentBase, ICreatureComponentAnimator
    {
        public MonoBehaviourAnimatorController AnimatorController => _animatorController;

        [SerializeField] private MonoBehaviourAnimatorController _animatorController;
        [SerializeField] private CreatureAnimationEventHandler _animationEventHandler;

        public override void Init(ICreatureComponentContainer componentContainer, ICreatureController creatureController)
        {
            _animationEventHandler.Setup(creatureController);
            _animatorController.Init();
        }
    }
}