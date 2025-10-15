using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Project.ObjectPools
{
    public interface IProjectPoolable : IPoolable
    {
        void OnCreated();
    }
}