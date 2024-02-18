using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector2 moveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    public Vector2 lookInput
    {
        get
        {
            var mPos = Input.mousePosition;
            var screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);

            return new Vector2(mPos.x - screenCenter.x, mPos.y - screenCenter.y);
        }
    }

    public bool attackInput => Input.GetKeyDown(KeyCode.Space);
}