using _Project.Scripts.Project.Enums;
using Defective.JSON;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.JSONParse;
using System.Text;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface ICreatureResourceBalanceModel : IBalanceModelWithIdBase
    { 
        CreatureResourceType ResourceType { get; }
        int MinValue { get; }
        int MaxValue { get; }
        int StartValue { get; }
    }

    public class CreatureResourceBalanceModel : BalanceModelWithIdBase, ICreatureResourceBalanceModel
    {
        public CreatureResourceType ResourceType => _resourceType;
        public int MinValue => _minValue;
        public int MaxValue => _maxValue;
        public int StartValue => _startValue;

        private CreatureResourceType _resourceType;
        private int _minValue;
        private int _maxValue;
        private int _startValue;

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            _id = json["id"].stringValue;
            _resourceType = parseHelper.ParseEnum(json, "type", CreatureResourceType.NONE);
            _minValue = json["min_value"].intValue;
            _maxValue = json["max_value"].intValue;
            _startValue = json["start_value"].intValue;
        }

        public override void DebugPrint()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"_id = {_id}");
            builder.AppendLine($"_resourceType = {_resourceType}");
            builder.AppendLine($"_minValue = {_minValue}");
            builder.AppendLine($"_maxValue = {_maxValue}");
            builder.AppendLine($"_startValue = {_startValue}");
            var result = builder.ToString();
            Debug.LogError(result);
        }
    }
}