using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public partial class Player
{
    public class Controls
    {
        public readonly float inputRotation = 45f;

        public Vector2 leftInput { get; private set; }
        public Vector2 rightInput { get; private set; }

        public bool slashButtonPressed { get; private set; }

        public void CheckInput(Vector3 position)
        {
            leftInput = GetLeftInput();
            rightInput = GetRightInput(position);
            slashButtonPressed = GetSlashButtonPressed();
        }

        private Vector2 GetLeftInput()
        {
            return Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
        }

        private Vector2 GetRightInput(Vector3 position)
        {
            return (Input.mousePosition - Camera.main.WorldToScreenPoint(position)).normalized;
        }

        private bool GetSlashButtonPressed()
        {
            return Input.GetButtonDown("Fire1");
        }
    }
}
