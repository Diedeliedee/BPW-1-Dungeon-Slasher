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

            public override void OnStart()
            {
                SetBehaviors(new Pursue(m_lookAheadTime, m_target));
            }

            public override void OnTick()
            {
                var targetPosition = Calc.VectorToFlat(m_target.position);

                if (Vector2.Distance(blackBoard.flatPosition, targetPosition) < m_attackDistance)
                {
                    SwitchToState<Attack>().SetAttack(targetPosition - blackBoard.flatPosition, GetType());
                    return;
                }

                TickMovement();
            }

            public override void OnDrawGizmos()
            {
                base.OnDrawGizmos();

                GizmoTools.DrawOutlinedDisc(blackBoard.transform.position, m_attackDistance, Color.red, Color.white, 0.15f);
            }
        }
    }
}
