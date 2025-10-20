using _Project.Scripts.Project.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Configs;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Panels;

namespace _Project.Scripts.Project.Configs.Windows
{
    [CreateAssetMenu(fileName = "PanelConfig", menuName = "Project/Configs/Project/Panel Config")]
    public class PanelConfig : ScriptableObject, IPanelsConfig
    {
        [SerializeField] private PanelWindow[] _projectPanels;
        [SerializeField] private PanelWindow[] _startScenePanels;
        [SerializeField] private PanelWindow[] _gameScenePanels;

        public void Init()
        {
        }

        public IReadOnlyCollection<PanelWindow> GetWindowsList(IWindowsConfigGetObjectBase getObject)
        {
            var getObj = (WindowConfigGetObject)getObject;

            switch (getObj.SceneName)
            {
                case SceneName.StartScene:
                    return _startScenePanels;

                case SceneName.GameScene:
                    return _gameScenePanels;
            }

            return _projectPanels;
        }
    }
}