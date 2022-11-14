using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_maxSpeed = 10f;
    [SerializeField] private float m_timeToMax = 0.25f;

    private Vector2 m_velocity = Vector2.zero;
    private Vector2 m_acceleration = Vector2.zero;

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        MoveWith(GetVelocity(GetInput(), m_maxSpeed, deltaTime), deltaTime);
    }

    /// <returns>The desired velocity altered by the passed in input.</returns>
    private Vector2 GetVelocity(Vector2 input, float speed, float deltaTime)
    {
        return Vector2.SmoothDamp(m_velocity, input.normalized * speed, ref m_acceleration, m_timeToMax, Mathf.Infinity, deltaTime);
    }

    /// <returns>The unity input in a Vector2.</returns>
    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    /// <summary>
    /// Relocates the transform with the given velocity, applied with the passed in delta time.
    /// </summary>
    private void MoveWith(Vector2 velocity, float deltaTime)
    {
        var position = transform.position;

        position.x += velocity.x * deltaTime;
        position.z += velocity.y * deltaTime;
        transform.position = position;

        m_velocity = velocity;
    }
}
