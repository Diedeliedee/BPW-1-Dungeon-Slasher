using UnityEngine;

public class InputReader : MonoBehaviour
{
    private InputProvider m_actions = null;

    public Vector2 moveInput => m_actions.Player.Movement.ReadValue<Vector2>();
    public Vector2 lookInput
    {
        get
        {
            var mPos = Input.mousePosition;
            var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

            return new Vector2(mPos.x - screenCenter.x, mPos.y - screenCenter.y);
        }
    }
    public bool attackInput => m_actions.Player.Attack.triggered;

    private void Awake()
    {
        m_actions = new InputProvider();
        m_actions.Enable();
    }
}