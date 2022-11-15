using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [Min(0f)] [SerializeField] private float m_maxSpeed = 10f;
    [Min(0f)] [SerializeField] private float m_timeToMax = 0.25f;

    private Vector2 m_velocity = Vector2.zero;
    private Vector2 m_acceleration = Vector2.zero;

    private void TickMovement(float _deltaTime)
    {
        MoveWith(GetVelocity(GetInput(), m_maxSpeed, _deltaTime), _deltaTime);
    }

    /// <returns>The desired velocity altered by the passed in input.</returns>
    private Vector2 GetVelocity(Vector2 _input, float _speed, float _deltaTime)
    {
        return Vector2.SmoothDamp(m_velocity, _input.normalized * _speed, ref m_acceleration, m_timeToMax, Mathf.Infinity, _deltaTime);
    }

    /// <returns>The unity input in a Vector2.</returns>
    private Vector2 GetInput()
    {
        var rawInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        return Calc.RotateVector2(rawInput, 45f);
    }

    /// <summary>
    /// Relocates the transform with the given velocity, applied with the passed in delta time.
    /// </summary>
    private void MoveWith(Vector2 _velocity, float _deltaTime)
    {
        var threeDimensionalVelocity = new Vector3(_velocity.x, 0f, _velocity.y);

        m_controller.Move(threeDimensionalVelocity * _deltaTime);
        m_velocity = _velocity;
    }
}
