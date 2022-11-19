using UnityEngine;

namespace DungeonSlasher.Agents
{
    [CreateAssetMenu(fileName = "Agent Settings", menuName = "ScriptableObjects/Agents/Agent Settings", order = 0)]
    public class AgentSettings : ScriptableObject
    {
        [Header("Movement:")]
        public float maxSpeed = 10f;
        public float baseGrip = 0.1f;
        public float baseDrag = 1f;

        [Header("Attacking")]
        public float attackDistance = 5f;
        public float attackTime = 0.1f;
        public AnimationCurve distanceOverTime = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    }
}
