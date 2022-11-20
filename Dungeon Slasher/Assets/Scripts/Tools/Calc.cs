﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Static class that holds some simple, but common calculation functions.
/// </summary>
public static class Calc
{
    /// <returnsThe 'current' integer, with 1 added to it. It loops back to zero once it reached it's max.></returns>
    public static int ScrollOne(int current, int max)
    {
        current++;
        if (current > max) current = 0;
        return current;
    }

    /// <returns>The 'current' integer, with 'amount' added to it, but loops around with 'max' as it's max value.</returns>
    public static int Scroll(int current, int amount, int max)
    {
        current += amount;
        current %= max;
        return current;
    }

    /// <returns>The passed in 'color', but with the passed in 'opacity'.</returns>
    public static Color SetOpacity(Color color, float opacity)
    {
        return new Color(color.r, color.g, color.b, opacity);
    }

    /// <returns>The passed in 'current' float, but swapped around according to the 'max' value.</returns>
    public static float Reverse(float current, float max)
    {
        current *= -1;
        current /= max;
        current += 1;
        current *= max;
        return current;
    }

    /// <returns>The passed in 0-1 integer, but swapped in value.</returns>
    public static float Reverse01(float current)
    {
        return current * -1 + 1;
    }

    /// <returns>The passed in 2D direction vector rotated with the passed in amount of degrees.</returns>
    public static Vector2 RotateVector2(Vector2 vector, float degrees)
    {
        var radians = degrees * Mathf.Deg2Rad;
        var newVector = Vector2.zero;

        newVector.x = vector.x * Mathf.Cos(radians) - vector.y * Mathf.Sin(radians);
        newVector.y = vector.x * Mathf.Sin(radians) + vector.y * Mathf.Cos(radians);
        return newVector;
    }

    /// <returns>The passed in 3D vector turned into a 2D vecotr with X, and Z.</returns>
    public static Vector2 VectorToFlat(Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }

    /// <returns>The passed in flat 2D vector, turned into a 3D vector applied by X, and Z.</returns>
    public static Vector3 FlatToVector(Vector2 vector, float height = 0f)
    {
        return new Vector3(vector.x, height, vector.y);
    }

    /// <returns>Whether a list of colliders containts the desired component.</returns>
    public static bool Contains<T>(out T[] containingComponents, params Collider[] colliders)
    {
        var componentList = new List<T>();

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out T component)) componentList.Add(component);
        }
        containingComponents = componentList.ToArray();
        return containingComponents.Length > 0;
    }
}
