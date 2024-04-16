using UnityEngine;

/// <summary>
/// Class which holds parameter values, just like in the settings class.
/// However, these values can change at the hand of upgrades.
/// And thus there variables should be referenced from here, and not the settings class.
/// </summary>
public class PlayerConfiguration
{
    public Vector2 savedPosition;
    public float savedAngle;

    public float speed;

    public int leftAttackType;
    public int rightAttackType;

    public System.Type leftAttackState
    {
        set
        {
            if (value == typeof(Player.StartupLeft))    { leftAttackType = 0; return; }
            if (value == typeof(Player.AttackLeft))     { leftAttackType = 1; return; }
        }
        get
        {
            return leftAttackType switch
            {
                1 => typeof(Player.AttackLeft),
                _ => typeof(Player.StartupLeft),
            };
        }
    }
    public System.Type rightAttackState
    {
        set
        {
            if (value == typeof(Player.StartupRight))   { rightAttackType = 0; return; }
            if (value == typeof(Player.AttackRight))    { rightAttackType = 1; return; }
        }
        get
        {
            return rightAttackType switch
            {
                1 => typeof(Player.AttackRight),
                _ => typeof(Player.StartupRight),
            };
        }
    }
}
