using UnityEngine;

public abstract class AbilityType : ScriptableObject
{
    [SerializeField] private string m_name;
    [SerializeField] [TextArea(1, 3)] private string m_description;

    public new string name => m_name;
    public string description => m_description;

    public abstract void ApplyAbility(Player _player);
}
