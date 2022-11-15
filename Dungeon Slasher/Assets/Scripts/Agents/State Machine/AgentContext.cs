using System.Collections.Generic;
using UnityEngine;

public struct AgentContext
{
    //  Standard:
    public float deltaTime;
    public GameObject gameObject;
    public Transform transform;

    //  Common:
    public CharacterController controller;

    public AgentContext(float deltaTime, GameObject gameObject, CharacterController controller)
    {
        this.deltaTime = deltaTime;
        this.gameObject = gameObject;
        this.transform = gameObject.transform;

        this.controller = controller;
    }
}
