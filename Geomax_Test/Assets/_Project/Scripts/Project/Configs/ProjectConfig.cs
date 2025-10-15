using UnityEngine;

namespace _Project.Scripts.Project.Configs
{
    public interface IProjectConfig
    {
        void Init();
    }

    public abstract class ProjectConfig : ScriptableObject, IProjectConfig
    {
        public abstract void Init();
    }
}