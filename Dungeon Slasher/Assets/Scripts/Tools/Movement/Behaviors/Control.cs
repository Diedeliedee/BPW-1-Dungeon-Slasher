using UnityEngine;

namespace Dodelie.Tools
{
    class Control : Behavior
    {
        private Controls m_controls = null;
        private float m_inputRotation = 0f;

        public Control(Controls controls, float inputRotation = 0f)
        {
            m_controls = controls;
            m_inputRotation = inputRotation;
        }

        public override Vector2 GetDesiredVelocity(Context context)
        {
            return Calc.RotateVector2(m_controls.leftInput, m_inputRotation) * context.speed;
        }
    }
}
