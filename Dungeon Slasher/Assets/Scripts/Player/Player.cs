using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    //  Run-time Variables:
    private State m_state = State.Moving;

    //  Reference:
    private CharacterController m_controller = null;

    //  Properties:
    private Vector2 Position { get => new Vector2(transform.position.x, transform.position.z); set => transform.position = new Vector3(value.x, transform.position.y, value.y); }

    private void Awake()
    {
        m_controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) StartAttack(GetInput());
        switch (m_state)
        {
            case State.Moving: TickMovement(deltaTime); break;
            case State.Attacking: TickAttack(deltaTime); break;
        }
    }

    private enum State { Moving, Attacking }
}
