using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Project.ObjectPools
{
    [Serializable]
    public class ObjectPoolContainerItem
    {
        public GameObject Prefab => _prefab;
        public Transform Container => _container;
        public int PoolInitialSize => _poolInitialSize;

        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private int _poolInitialSize;
    }
}