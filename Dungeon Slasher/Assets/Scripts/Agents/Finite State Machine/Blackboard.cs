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
            public readonly CharacterController controller;

            public Vector2 flatPosition { get => new Vector2(transform.position.x, transform.position.z); set => transform.position = new Vector3(value.x, transform.position.y, value.y); }

            public Blackboard(GameObject gameObject, CharacterController controller)
            {
                this.gameObject = gameObject;
                this.transform = gameObject.transform;
                this.controller = controller;
            }

            public void UpdateBlackboard(float deltaTime)
            {
                this.deltaTime = deltaTime;
            }
        }
    }
}