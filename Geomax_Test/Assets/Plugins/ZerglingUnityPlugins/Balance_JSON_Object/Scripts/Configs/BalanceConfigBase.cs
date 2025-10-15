using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.Configs
{
    public interface IBalanceConfig
    {
        TextAsset BalanceFile { get; }

#if UNITY_EDITOR
        string GoogleSheetId { get; }
#endif

        BalanceGoogleSheetPage[] GoogleSheetPages { get; }

        void ParseBalance();

        public enum ParseFuncType
        {
            NONE = 0,
            AsConfig = 1,
            AsDictionary = 2,
            Custom = 3,
        }
    }

    /// <summary>
    /// не забудь в дочернем классе добавить
    /// [CreateAssetMenu(fileName = "T.Name", menuName = "Configs/T.Name")]
    /// чтобы добавить пункт меню для создания конфига
    /// </summary>
    public abstract class BalanceConfigBase : ScriptableObject, IBalanceConfig
    {
        public TextAsset BalanceFile => _balanceFile;

#if UNITY_EDITOR
        public string GoogleSheetId => _googleSheetId;
#endif
        public BalanceGoogleSheetPage[] GoogleSheetPages => _googleSheetPages;

        [SerializeField] private TextAsset _balanceFile;

#if UNITY_EDITOR
        [SerializeField] private string _googleSheetId;
#endif

        [SerializeField] private BalanceGoogleSheetPage[] _googleSheetPages;

        public abstract void ParseBalance();
    }

    [Serializable]
    public class BalanceGoogleSheetPage
    {
        public string PageName => _pageName;
        public Object TargetBalanceStorage => _targetBalanceStorage;
        public bool DebugPrintOnInit => _debugPrintOnInit;
        public IBalanceConfig.ParseFuncType ParseFuncType => _parseFuncType;

        [SerializeField] private string _pageName;
        [SerializeField] private Object _targetBalanceStorage;
        [SerializeField] private bool _debugPrintOnInit;
        [SerializeField] private IBalanceConfig.ParseFuncType _parseFuncType;
    }
}
