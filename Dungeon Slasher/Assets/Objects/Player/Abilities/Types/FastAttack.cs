using UnityEngine;

[CreateAssetMenu(fileName = "Fast Attack", menuName = "Abilities/Fast Attack", order = 2)]
public class FastAttack : AbilityType
{
    public override void ApplyAbility(Player _player)
    {
        _player.configuration.leftAttackState   = typeof(Player.AttackLeft);
        _player.configuration.rightAttackState  = typeof(Player.AttackRight);
    }
}