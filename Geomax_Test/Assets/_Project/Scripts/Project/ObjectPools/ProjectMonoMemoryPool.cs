using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Project.ObjectPools
{
    public class ProjectMonoMemoryPool<TProjectPoolable> : MonoMemoryPool<TProjectPoolable> where TProjectPoolable : Component, IProjectPoolable
    {
        protected override void OnCreated(TProjectPoolable item)
        {
            base.OnCreated(item);
            item.OnCreated();
        }

        protected override void OnSpawned(TProjectPoolable item)
        {
            base.OnSpawned(item);
            item.OnSpawned();
        }

        protected override void OnDespawned(TProjectPoolable item)
        {
            base.OnDespawned(item);
            item.OnDespawned();
        }
    }
}