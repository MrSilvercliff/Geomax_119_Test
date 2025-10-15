using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Basics;

namespace ZerglingUnityPlugins.WindowsManagerAsync.Scripts.Configs
{
    public interface IWindowsConfig<TWindow> where TWindow : IWindow
    {
        IReadOnlyCollection<TWindow> GetWindowsList(IWindowsConfigGetObjectBase getObject);
    }

    public interface IWindowsConfigGetObjectBase
    {
    }
}

