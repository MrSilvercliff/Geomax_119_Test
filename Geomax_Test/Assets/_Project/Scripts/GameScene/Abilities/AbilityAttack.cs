using _Project.Scripts.Project.Services.Balance.Models;
using UnityEngine;

namespace _Project.Scripts.GameScene.Abilities
{
    public interface IAbilityAttack : IAbility
    { 
        int Damage { get; }
        float CooldownSeconds { get; }
    }

    public class AbilityAttack : Ability<AbilityAttack>, IAbilityAttack
    {
        public int Damage => _balanceModel.IntValue1;
        public float CooldownSeconds => _balanceModel.FloatValue1;

        public AbilityAttack(IAbilityBalanceModel balanceModel) : base(balanceModel)
        {
        }
    }
}