using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;
using Joeri.Tools.Structure;

public partial class Enemy
{
    public class Death : FlexState<Enemy>
    {
        private Timer m_timer = null;
        private ShakeInstancer m_shake = null;

        private Settings settings { get => GetSettings<Settings>(); }

        public Death(Enemy root, Settings settings) : base(root, settings) { }

        public override void OnEnter()
        {
            m_timer = new Timer(settings.animation.length);
            m_shake = new ShakeInstancer(root.transform.position, settings.magnitude, 60, settings.duration);

            root.m_dyingSed = true;
            root.CrossFadeAnimation(settings.animation, 0f);
        }

        public override void OnTick(float deltaTime)
        {
            root.transform.position = m_shake.GetPosition(deltaTime);
            if (m_timer.HasReached(deltaTime))
            {
                m_timer = null;
                GameManager.instance.entities.DespawnEnemy(root);
            }
        }

        [System.Serializable]
        public class Settings : FlexState<Enemy>.Settings
        {
            public AnimationClip animation;
            public float magnitude;
            public float duration;
        }
    }
}
