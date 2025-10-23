using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.Monobeh
{
    public interface IProjectMonoBehaviour
    {
        Transform Transform { get; }
        bool ActiveInHierarchy { get; }
        int InstanceID { get; }

        void SetActive(bool active);
        void RefreshInstanceId();
    }

    public class ProjectMonoBehaviour : MonoBehaviour, IProjectMonoBehaviour
    {
        public Transform Transform => _transform;
        public bool ActiveInHierarchy => gameObject.activeInHierarchy;
        public int InstanceID => _instanceId;

        [Header("PROJECT MONO BEHAVIOUR")]
        [SerializeField] private Transform _transform;

        private int _instanceId;

        private void Awake()
        {
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void RefreshInstanceId()
        {
            _instanceId = gameObject.GetInstanceID();
        }
    }
}