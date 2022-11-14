using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField] private float m_attackDistance = 5f;
    [SerializeField] private float m_attackTime = 5f;
    [SerializeField] private float m_squaredQuiteRange = 5f;

    private Vector2 m_endPosition = Vector2.zero;

    private void StartAttack(Vector2 _input)
    {
        m_velocity = Vector2.zero;
        m_state = State.Attacking;
        m_endPosition = Position + _input.normalized * m_attackDistance;
    }

    private void TickAttack(float _deltaTime)
    {
        var position = Position;
        Position = Vector2.SmoothDamp(position, m_endPosition, ref m_velocity, m_attackTime, Mathf.Infinity, _deltaTime);

        if ((m_endPosition - position).sqrMagnitude > m_squaredQuiteRange) return;
        m_state = State.Moving;
    }
}
