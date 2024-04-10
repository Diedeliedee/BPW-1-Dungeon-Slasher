using UnityEngine;

public class SpeedIncrease : AbilityType
{
    [Space]
    [SerializeField] private float m_amount = 1f;

    public override void ApplyAbility(Player _player)
    {
        _player.configuration.speed += m_amount;
    }
}
