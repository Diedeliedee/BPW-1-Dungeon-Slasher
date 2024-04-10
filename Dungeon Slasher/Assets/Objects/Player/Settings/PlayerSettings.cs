using UnityEngine;

[CreateAssetMenu(fileName = "New Player Settings", menuName = "Configurations/Player Settings", order = 0)]
public class PlayerSettings : ScriptableObject
{
    [SerializeField] private float m_speed  = 10f;
    [SerializeField] private float m_grip   = 20f;

    [SerializeField] private float m_attackVelocity         = 45f;
    [SerializeField] private float m_attackTime             = 0.15f;
    [SerializeField] private float m_recoverSpeedMultiplier = 0.5f;

    public float speed  => m_speed;
    public float grip   => m_grip;

    public float attackVelocity         => m_attackVelocity;
    public float attackTime             => m_attackTime;
    public float recoverSpeedMultiplier => m_recoverSpeedMultiplier;
}
