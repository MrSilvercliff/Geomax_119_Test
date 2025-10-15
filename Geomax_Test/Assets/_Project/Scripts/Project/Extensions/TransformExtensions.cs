using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Extensions
{
    public static class TransformExtensions
    {
        public static void ResetLocalZ(this Transform transform)
        {
            var localPosition = transform.localPosition;
            localPosition.z = 0;
            transform.localPosition = localPosition;
        }

        public static void ResetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        public static void ResetLocalRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        public static void ResetLocalScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }
    }
}