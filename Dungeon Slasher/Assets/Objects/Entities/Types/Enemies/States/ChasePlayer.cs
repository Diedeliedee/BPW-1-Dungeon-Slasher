using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Structure;
using Joeri.Tools.Utilities;
using Joeri.Tools.Debugging;
using Joeri.Tools.Movement;

public partial class Enemy
{
    public class ChasePlayer : FlexState<Enemy>
    {
        public Settings settings { get => GetSettings<Settings>(); }

        public ChasePlayer(Enemy root, Settings settings) : base(root, settings) { }

        public override void OnEnter()
        {
            root.movement.SetBehaviors(new Pursue(settings.lookAheadTime, root.player.transform));
            root.CrossFadeAnimation(settings.animation, 0.55f);
        }

        public override void OnTick(float deltaTime)
        {
            var targetPosition = Vectors.VectorToFlat(root.player.transform.position);

            if ((targetPosition - root.flatPosition).sqrMagnitude < settings.sqrAttackDistance)
            {
                SwitchToState<Attack>().Setup(targetPosition - root.flatPosition);
                return;
            }

            root.movement.ApplyBehaviorVelocity(deltaTime);
        }

        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            GizmoTools.DrawOutlinedDisc(root.transform.position, Mathf.Pow(settings.sqrAttackDistance, 2), Color.red, Color.white, 0.15f);
        }
        
        [System.Serializable]
        public class Settings : FlexState<Enemy>.Settings
        {
            public AnimationClip animation;
            public float lookAheadTime = 0.5f;
            public float sqrAttackDistance = 1.5f;
        }
    }
}
