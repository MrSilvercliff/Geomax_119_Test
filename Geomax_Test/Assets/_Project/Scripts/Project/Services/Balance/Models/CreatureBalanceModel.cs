using _Project.Scripts.Project.Enums;
using System.Collections.Generic;
using System.Text;
using Defective.JSON;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.BalanceStorage;
using Plugins.ZerglingUnityPlugins.Balance_Total_JSON.Scripts.JSONParse;
using UnityEngine;

namespace _Project.Scripts.Project.Services.Balance.Models
{
    public interface ICreatureBalanceModel : IBalanceModelWithIdBase
    { 
        CreatureType CreatureType { get; }
        string Name { get; }
        string PrefabId { get; }
        StateMachineType StateMachineType { get; }
        IReadOnlyList<string> Resources { get; }
        string BasicAttackAbilityId { get; }
        IReadOnlyList<string> Abilities { get; }
        IReadOnlyList<string> Effects { get; }
    }

    public class CreatureBalanceModel : BalanceModelWithIdBase, ICreatureBalanceModel
    {
        public CreatureType CreatureType => _creatureType;
        public string Name => _name;
        public string PrefabId => _prefabId;
        public StateMachineType StateMachineType => _stateMachineType;
        public IReadOnlyList<string> Resources => _resources;
        public string BasicAttackAbilityId => _basicAttackAbilityId;
        public IReadOnlyList<string> Abilities => _abilities;
        public IReadOnlyList<string> Effects => _effects;

        private CreatureType _creatureType;
        private string _name;
        private string _prefabId;
        private StateMachineType _stateMachineType;
        private List<string> _resources;
        private string _basicAttackAbilityId;
        private List<string> _abilities;
        private List<string> _effects;
        
        protected override void OnTrySetup(JSONObject json, IJSONParseHelper parseHelper)
        {
            _id = json["id"].stringValue;
            _creatureType = parseHelper.ParseEnum(json, "type", CreatureType.NONE);
            _name = json["name"].stringValue;
            _prefabId = json["prefab_id"].stringValue;
            _stateMachineType = parseHelper.ParseEnum(json, "state_machine_type", StateMachineType.NONE);
            _resources = parseHelper.ParseList<string>(json, "resources");
            _basicAttackAbilityId = json["basic_attack_ability_id"].stringValue;
            _abilities = parseHelper.ParseList<string>(json, "abilities");
            _effects = parseHelper.ParseList<string>(json, "effects");
        }

        public override void DebugPrint()
        {
            var builder = new StringBuilder();
            
            builder.AppendLine($"Id = {Id}");
            builder.AppendLine($"_creatureType = {_creatureType}");
            builder.AppendLine($"_name = {_name}");
            builder.AppendLine($"_prefabId = {_prefabId}");
            builder.AppendLine($"_stateMachineType = {_stateMachineType}");
            builder.AppendLine($"_basicAttackAbilityId = {_basicAttackAbilityId}");

            builder.AppendLine($"");

            builder.AppendLine($"Resources:");
            for (int i = 0; i < _resources.Count; i++)
                builder.AppendLine($"_resources[{i}] = {_resources[i]}");

            builder.AppendLine($"");

            builder.AppendLine($"Abilities:");
            for (int i = 0; i < _abilities.Count; i++)
                builder.AppendLine($"_abilities[{i}] = {_abilities[i]}");

            builder.AppendLine($"");

            builder.AppendLine($"Effects:");
            for (int i = 0; i < _effects.Count; i++)
                builder.AppendLine($"_effects[{i}] = {_effects[i]}");

            Debug.LogError(builder.ToString());
        }
    }
}