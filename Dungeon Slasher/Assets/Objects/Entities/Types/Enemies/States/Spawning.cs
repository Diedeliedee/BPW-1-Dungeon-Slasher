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
    public class Spawning : FlexState<Enemy>
    {
        private Timer m_timer = null;

        private Settings settings { get => GetSettings<Settings>(); }

        public Spawning(Enemy root, Settings settings) : base(root, settings) { }

        public override void OnEnter()
        {
            m_timer = new Timer(settings.animation.length);
            root.CrossFadeAnimation(settings.animation, 0f);
        }

        public override void OnTick(float deltaTime)
        {
            if (m_timer.HasReached(deltaTime)) return;
            SwitchToState(typeof(ChasePlayer));
        }

        public override void OnExit()
        {
            m_timer = null;
        }

        [System.Serializable]
        public class Settings : FlexState<Enemy>.Settings
        {
            public AnimationClip animation;
        }
    }
}
