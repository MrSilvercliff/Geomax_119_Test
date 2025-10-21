using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Animations
{
    public abstract class AnimationEventHandler : MonoBehaviour
    {
        public abstract void OnAnimationEvent();
        public abstract void OnAnimationEventInt(int value);
        public abstract void OnAnimationEventFloat(float value);
        public abstract void OnAnimationEventString(string value);
    }
}