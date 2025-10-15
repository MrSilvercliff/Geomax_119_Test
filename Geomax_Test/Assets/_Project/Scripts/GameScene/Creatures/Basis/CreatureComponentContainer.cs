using _Project.Scripts.GameScene.Creatures.Basis.Components;
using _Project.Scripts.Project.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.Tools.Scripts.Interfaces.Sync;
using ZerglingUnityPlugins.Tools.Scripts.Mono;

namespace _Project.Scripts.GameScene.Creatures.Basis
{
    public interface ICreatureComponentContainer : IFlushable
    {
        void InitComponents(ICreatureController creatureController, IReadOnlyCollection<ICreatureComponentBase> components);
        T GetComponent<T>(CreatureComponentType componentType) where T : ICreatureComponentBase;
    }

    public class CreatureComponentContainer : ICreatureComponentContainer
    {
        private readonly Dictionary<CreatureComponentType, ICreatureComponentBase> _allComponents;

        public CreatureComponentContainer()
        {
            _allComponents = new();
        }

        public void InitComponents(ICreatureController creatureController, IReadOnlyCollection<ICreatureComponentBase> components)
        {
            foreach (var component in components) 
            {
                var componentType = component.ComponentType;
                _allComponents[componentType] = component;
                component.Init(this, creatureController);
            }
        }

        public bool Flush()
        {
            _allComponents.Clear();
            return true;
        }

        public T GetComponent<T>(CreatureComponentType componentType) where T : ICreatureComponentBase
        {
            var result = (T)_allComponents[componentType];
            return result;
        }
    }
}