﻿using UnityEngine;
using UnityEngine.Events;

public class AttackInstance
{
    public Vector2 movementDirection    = default;
    public Vector3 attackVelocity       = default;
    public int damage                   = 0;

    public UnityEvent onAttackEnd   = null;
}