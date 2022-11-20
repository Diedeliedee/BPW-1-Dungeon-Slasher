using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract class Behavior
    {
        public abstract Vector2 GetDesiredVelocity(Vector2 position, float speed);

        public virtual void DrawGizmos(Vector3 position) { }
    }
}
