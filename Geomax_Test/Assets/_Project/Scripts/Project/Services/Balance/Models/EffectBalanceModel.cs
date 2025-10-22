using _Project.Scripts.Project.Enums;
using Defective.JSON;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.JSONParse;
using System.Text;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface IEffectBalanceModel : IBalanceModelWithIdBase
    { 
        EffectTargetType TargetType { get; }
        EffectTriggerType TriggerType { get; }
        EffectDurationType DurationType { get; }
        EffectType EffectType { get; }
        EffectValueType EffectValueType { get; }
        
        float Parameter { get; }
        float DurationSeconds { get; }
        float TickTime { get; }
        float Probability { get; }
        string TriggerEffectId { get; }

        bool Visible { get; }
    }

    public class EffectBalanceModel : BalanceModelWithIdBase, IEffectBalanceModel
    {
        public EffectTargetType TargetType => _targetType;
        public EffectTriggerType TriggerType => _triggerType;
        public EffectDurationType DurationType => _durationType;
        public EffectType EffectType => _effectType;
        public EffectValueType EffectValueType => _effectValueType;

        public float Parameter => _parameter;
        public float DurationSeconds => _durationSeconds;
        public float TickTime => _tickTime;
        public float Probability => _probability;
        public string TriggerEffectId => _triggerEffectId;

        public bool Visible => _visible;

        private EffectTargetType _targetType;
        private EffectTriggerType _triggerType;
        private EffectDurationType _durationType;
        private EffectType _effectType;
        private EffectValueType _effectValueType;

        private float _parameter;
        private float _durationSeconds;
        private float _tickTime;
        private float _probability;
        private string _triggerEffectId;

        private bool _visible;

        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            _id = json["id"].stringValue;

            _targetType = parseHelper.ParseEnum(json, "target_type", EffectTargetType.NONE);
            _triggerType = parseHelper.ParseEnum(json, "trigger_type", EffectTriggerType.NONE);
            _durationType = parseHelper.ParseEnum(json, "duration_type", EffectDurationType.NONE);
            _effectType = parseHelper.ParseEnum(json, "effect_type", EffectType.NONE);
            _effectValueType = parseHelper.ParseEnum(json, "value_type", EffectValueType.NONE);

            _parameter = json["parameter"].floatValue;
            _durationSeconds = json["duration_seconds"].floatValue;
            _tickTime = json["tick_time"].floatValue;
            _probability = json["probability"].floatValue;
            _triggerEffectId = json["trigger_effect_id"].stringValue;

            _visible = json["visible"].boolValue;
        }

        public override void DebugPrint()
        {
            var builder = new StringBuilder();

            builder.AppendLine($"Id = {Id}");

            builder.AppendLine($"_targetType = {_targetType}");
            builder.AppendLine($"_triggerType = {_triggerType}");
            builder.AppendLine($"_durationType = {_durationType}");
            builder.AppendLine($"_effectType = {_effectType}");
            builder.AppendLine($"_effectValueType = {_effectValueType}");

            builder.AppendLine($"_parameter = {_parameter}");
            builder.AppendLine($"_durationSeconds = {_durationSeconds}");
            builder.AppendLine($"_tickTime = {_tickTime}");
            builder.AppendLine($"_probability = {_probability}");
            builder.AppendLine($"_triggerEffectId = {_triggerEffectId}");

            builder.AppendLine($"_visible = {_visible}");

            var result = builder.ToString();
            Debug.Log(result);
        }
    }
}