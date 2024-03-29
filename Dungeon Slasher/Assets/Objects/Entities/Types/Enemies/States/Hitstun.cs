using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;
using Joeri.Tools.Debugging;
using Joeri.Tools.Movement;

public partial class Enemy
{
    public class Hitstun : FlexState<Enemy>
    {
        private Timer m_timer = null;
        private ShakeInstancer m_shake = null;

        public Settings settings { get => GetSettings<Settings>(); }

        public Hitstun(Enemy root, Settings settings) : base(root, settings) { }

        public override void OnEnter()
        {
            m_timer = new Timer(settings.duration);
            m_shake = new ShakeInstancer(root.transform.position, settings.magnitude, 60, settings.duration);

            root.m_animator.speed = 0;
        }

        public override void OnTick(float deltaTime)
        {
            root.transform.position = m_shake.GetPosition(deltaTime);
            if (m_timer.HasReached(deltaTime))
            {
                SwitchToState(typeof(ChasePlayer));
            }
        }

        public override void OnExit()
        {
            root.m_animator.speed = 1;

            m_timer = null;
            m_shake = null;
        }

        [System.Serializable]
        public class Settings : FlexState<Enemy>.Settings
        {
            public float magnitude;
            public float duration;
        }
    }
}
