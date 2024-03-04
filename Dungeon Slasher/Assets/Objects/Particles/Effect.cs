using Joeri.Tools.Patterns.ObjectPool;
using UnityEngine;

public class Effect : PoolItem
{
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    protected override void OnSpawn()
    {
        m_animator.Play(0, -1, 0f);
    }

    protected override void OnDespawn()
    {

    }
}