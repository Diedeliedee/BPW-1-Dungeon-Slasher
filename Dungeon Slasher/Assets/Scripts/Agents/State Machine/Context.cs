using System.Collections.Generic;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    public abstract partial class State
    {
        public struct Context
        {
            //  Standard:
            public readonly float deltaTime;
            public readonly GameObject gameObject;
            public readonly Transform transform;

            //  Common:
            public readonly CharacterController controller;

            public Context(float deltaTime, GameObject gameObject, CharacterController controller)
            {
                this.deltaTime = deltaTime;
                this.gameObject = gameObject;
                this.transform = gameObject.transform;

                this.controller = controller;
            }
        }
    }
}
