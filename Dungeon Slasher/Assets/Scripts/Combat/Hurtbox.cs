using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Hurtbox : MonoBehaviour
{
    //  Events:
    public System.Action<IDamagable, Vector3> onHit;

    //  Cache:
    private Vector3 m_previousPosition  = default;
    private Vector3 m_velocity          = default;

    private void Start()
    {
        m_previousPosition = transform.position;
    }

    private void Update()
    {
        m_velocity          = (transform.position - m_previousPosition) / Time.deltaTime;
        m_previousPosition  = transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out IDamagable _damagable)) return;
        onHit?.Invoke(_damagable, m_velocity);
    }
}