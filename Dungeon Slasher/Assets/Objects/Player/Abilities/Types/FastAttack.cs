public class FastAttack : AbilityType
{
    public override void ApplyAbility(Player _player)
    {
        _player.configuration.leftAttackState   = typeof(Player.AttackLeft);
        _player.configuration.rightAttackState  = typeof(Player.AttackRight);
    }
}