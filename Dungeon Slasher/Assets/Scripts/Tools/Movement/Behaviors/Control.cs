using UnityEngine;

namespace Dodelie.Tools
{
    class Control : Behavior
    {
        private Controls m_controls = null;

        public Control(Controls controls)
        {
            m_controls = controls;
        }

        public override Vector2 GetDesiredVelocity(Context context)
        {
            return m_controls.leftInput * context.speed;
        }
    }
}
