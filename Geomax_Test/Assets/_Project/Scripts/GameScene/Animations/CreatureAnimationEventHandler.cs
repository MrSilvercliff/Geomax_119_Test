using _Project.Scripts.GameScene.Creatures.Basis;
using _Project.Scripts.Project.Animations;
using _Project.Scripts.Project.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Log;
using ZerglingUnityPlugins.Tools.Scripts.Utils;

namespace _Project.Scripts.GameScene.Animations
{
    public class CreatureAnimationEventHandler : AnimationEventHandler
    {
        private ICreatureController _creatureController;

        public void Setup(ICreatureController creatureController)
        {
            _creatureController = creatureController;
        }

        public override void OnAnimationEvent()
        {
            Debug.LogError($"[{GetType().Name}] OnAnimationFinish");
        }

        public override void OnAnimationEventInt(int value)
        {
            Debug.LogError($"[{GetType().Name}] OnAnimationFinish int");
        }

        public override void OnAnimationEventFloat(float value)
        {
            Debug.LogError($"[{GetType().Name}] OnAnimationFinish float");
        }

        public override void OnAnimationEventString(string stringValue)
        {
            //Debug.LogError($"[{GetType().Name}] OnAnimationFinish string");
            var creatureAnimationEvent = StringUtils.ParseEnum(stringValue, CreatureAnimationEvent.NONE);

            if (creatureAnimationEvent == CreatureAnimationEvent.NONE)
            {
                LogUtils.Error(this, $"[{_creatureController.Transform.gameObject.name}] creature animation event [{stringValue}] parse ERROR!");
                return;
            }

            _creatureController.OnAnimationEvent(creatureAnimationEvent);
        }
    }
}