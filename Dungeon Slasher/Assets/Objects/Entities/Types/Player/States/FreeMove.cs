using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;

public partial class Player
{
    public class FreeMove : FlexState<Player>
    {
        public Settings settings { get => GetSettings<Settings>(); }

        public FreeMove(Player root, Settings settings) : base(root, settings) { }

        public override void OnEnter()
        {
            root.CrossFadeAnimation(settings.animation, 0.3f);
        }

        public override void OnTick(float deltaTime)
        {
            if (root.m_controls.slashButtonPressed)
            {
                SwitchToState<LeftAttack>().Setup(root.m_controls.rightInput);
                return;
            }

            root.movement.ApplyInput(root.m_controls.leftInput, deltaTime);
            root.movement.RotateToDir(root.m_controls.rightInputWorldDir, deltaTime);
        }

        [System.Serializable]
        public class Settings : FlexState<Player>.Settings
        {
            public AnimationClip animation;
        }
    }
}
