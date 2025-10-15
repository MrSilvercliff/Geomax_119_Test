using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Animations
{
    public static class AnimatorStateHash
    {
        public static readonly int NONE = Animator.StringToHash("");

        public static readonly int Empty = Animator.StringToHash("Empty");

        public static readonly int Idle = Animator.StringToHash("Idle");
    }
}