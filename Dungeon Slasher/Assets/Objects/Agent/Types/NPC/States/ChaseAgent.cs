using Dodelie.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public partial class NPC
    {
        [System.Serializable]
        public class ChaseAgent : MovementState
        {
            [Space]
            [SerializeField] private float m_lookAheadTime = 0.5f;
            [SerializeField] private float m_attackDistance = 1.5f;
            [Space]
            [SerializeField] private Transform m_target = null;

            public override void Initialize(FSM<Agent> parent)
            {
                base.Initialize(parent);
                SetBehaviors(new Pursue(m_lookAheadTime, m_target));
            }

            public override void OnTick(float deltaTime)
            {
                var targetPosition = Calc.VectorToFlat(m_target.position);

                if (Vector2.Distance(root.flatPosition, targetPosition) < m_attackDistance)
                {
                    SwitchToState<AttackState>().InitiateAttack(targetPosition - root.flatPosition);
                    return;
                }

                TickMovement(deltaTime);
            }

            public override void OnDrawGizmos()
            {
                base.OnDrawGizmos();

                GizmoTools.DrawOutlinedDisc(root.transform.position, m_attackDistance, Color.red, Color.white, 0.15f);
            }
        }
    }
}
