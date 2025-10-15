using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.Configs;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.GoogleSheetParse;
using System;
using System.Collections;
using UnityEngine;

namespace _Project.Scripts.Application.Project.Services.Balance
{
    public class BalanceGoogleSheetParser : BalanceGoogleSheetParserBase
    {
        protected override void FillSheetsDictionary()
        {
            var sheets = _config.GoogleSheetPages;

            foreach (var sheet in sheets)
            {
                var pageName = sheet.PageName;
                var parseFuncType = sheet.ParseFuncType;
                Func<IEnumerator> parseFunc = null;

                switch (parseFuncType)
                {
                    case IBalanceConfig.ParseFuncType.AsConfig:
                        parseFunc = ParseAsIsConfig;
                        break;

                    case IBalanceConfig.ParseFuncType.AsDictionary:
                        parseFunc = ParseAsIsDictionary;
                        break;

                    case IBalanceConfig.ParseFuncType.Custom:
                        parseFunc = GetParseFunc(pageName);
                        break;

                    default:
                        Debug.LogError($"GOOGLE SHEET PARSE FUNCTION FOR {pageName} NOT SELECTED!");
                        break;
                }

                Debug.LogError($"{pageName} : {parseFunc.Method.Name}");
                _sheets[pageName] = parseFunc;
            }
        }

        private Func<IEnumerator> GetParseFunc(string pageName)
        {
            Debug.LogError($"PARSE FUNCTION FOR {pageName} NOT ASSIGNED!");
            return null;
        }
    }
}
