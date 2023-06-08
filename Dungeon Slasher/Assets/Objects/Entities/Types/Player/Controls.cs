using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

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
            return Vectors.RotateVector2(Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f), -Camera.main.transform.eulerAngles.y);
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
