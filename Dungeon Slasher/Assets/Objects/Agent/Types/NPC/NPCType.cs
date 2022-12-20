using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObjects/Agents/NPC Type", order = 1)]
public class NPCType : ScriptableObject
{
    public GameObject prefab    = null;
    [Space]
    public string enemyName     = "New Enemy";
    public float radius         = 0.3f;
    public Color definingColor  = Color.red;
}
