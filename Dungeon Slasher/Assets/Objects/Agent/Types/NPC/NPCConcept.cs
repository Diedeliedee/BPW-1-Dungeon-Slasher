using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Agents/NPC Type", order = 1)]
public class NPCConcept : ScriptableObject
{
    public GameObject prefab    = null;
    public Type type            = 0;
    [Space]
    public float radius         = 0.3f;
    public Color definingColor  = Color.red;

    public enum Type
    {
        Scraggler,
    }
}
