using System;
using UnityEngine;
using UnityEngine.Events;

namespace DungeonSlasher
{
    public class EventManager : MonoBehaviour
    {
        public UnityEvent onEnemyHit = null;
        public UnityEvent onPlayerHit = null;
    }
}
