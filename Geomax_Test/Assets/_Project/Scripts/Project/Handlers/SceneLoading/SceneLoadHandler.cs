using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Project.Handlers.SceneLoading
{
    public interface ISceneLoadHandler
    {
        void OnSceneLoadingProgress(float progress);
    }

    public class SceneLoadHandler : ISceneLoadHandler
    {
        public void OnSceneLoadingProgress(float progress)
        {
        }
    }
}