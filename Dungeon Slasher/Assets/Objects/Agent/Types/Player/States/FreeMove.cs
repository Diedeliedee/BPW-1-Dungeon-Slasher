using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public partial class Player
{
    public class FreeMove : FlexState<Player>
    {
        private Settings settings { get => base.settings as Settings; }

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
        }

        public class Settings : ISettings
        {
            public AnimationClip animation;
        }
    }
}
