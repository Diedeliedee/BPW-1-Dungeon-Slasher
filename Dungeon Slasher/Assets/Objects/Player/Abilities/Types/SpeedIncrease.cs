using UnityEngine;

[CreateAssetMenu(fileName = "Speed Increase", menuName = "Abilities/Speed Increase", order = 1)]
public class SpeedIncrease : AbilityType
{
    [Space]
    [SerializeField] private float m_amount = 1f;

    public override void ApplyAbility(Player _player)
    {
        _player.configuration.speed += m_amount;
        _player.movement.speed      = _player.configuration.speed;
    }
}
