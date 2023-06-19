using System;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public UnityEvent<int, int> onEnemyHit  = null;
    public UnityEvent<int, int> onPlayerHit = null;
    public UnityEvent onPlayerHeal = null;
}
