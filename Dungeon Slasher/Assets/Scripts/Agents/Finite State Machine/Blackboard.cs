using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class Agent
    {
        public class Blackboard
        {
            //  Run-time:
            public float deltaTime;

            //  Initialized:
            public readonly GameObject gameObject;
            public readonly Transform transform;
            public readonly Movement movement;
            public readonly Combat combat;

            public Vector2 flatPosition { get => new Vector2(transform.position.x, transform.position.z); set => transform.position = new Vector3(value.x, transform.position.y, value.y); }

            public Blackboard(GameObject gameObject, Movement movement, Combat combat)
            {
                this.gameObject = gameObject;
                this.transform = gameObject.transform;
                this.movement = movement;
                this.combat = combat;
            }

            public void UpdateBlackboard(float deltaTime)
            {
                this.deltaTime = deltaTime;
            }
        }
    }
}