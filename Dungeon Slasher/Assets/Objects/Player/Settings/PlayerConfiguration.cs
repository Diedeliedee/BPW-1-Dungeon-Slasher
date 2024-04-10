/// <summary>
/// Class which holds parameter values, just like in the settings class.
/// However, these values can change at the hand of upgrades.
/// And thus there variables should be referenced from here, and not the settings class.
/// </summary>
public class PlayerConfiguration
{
    public float speed;

    public System.Type leftAttackState; 
    public System.Type rightAttackState;
}
