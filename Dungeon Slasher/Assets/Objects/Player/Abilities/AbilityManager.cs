using UnityEngine;
using UnityEngine.Events;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<string, string> m_onAbilityAdded;

    private Player m_player = null;

    private void Awake()
    {
        m_player = GetComponent<Player>();
    }

    public void ApplyAbility(AbilityType _modifier)
    {
        _modifier.ApplyAbility(m_player);
        m_onAbilityAdded.Invoke(_modifier.name, _modifier.description);
    }
}
