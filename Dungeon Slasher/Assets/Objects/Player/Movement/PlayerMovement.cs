using Joeri.Tools.Movement;
using Joeri.Tools.Utilities;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed { get; set; }
    public float grip { get; set; }
    public float drag { get; set; }
    public Vector2 velocity
    {
        get => m_movement.velocity;
        set => m_movement.velocity = value;
    }

    private Accel.Flat m_movement = new();
    private CharacterController m_controller = null;

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
    }

    public void MoveInDirection(Vector2 _input)
    {
        m_controller.Move(m_movement.CalculateVelocity(_input, speed, grip, Time.deltaTime).Cubular() * Time.deltaTime);
    }

    public void IterateInDirection()
    {
        m_controller.Move(m_movement.CalculateVelocity(drag, Time.deltaTime).Cubular() * Time.deltaTime);
    }
}
