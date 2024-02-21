using UnityEngine;
using UnityEngine.Events;

public class StunHandler : MonoBehaviour, IStunnable
{
    [SerializeField] private UnityEvent m_onStunCall;

    public void Stun()
    {
        m_onStunCall.Invoke();
    }
}
