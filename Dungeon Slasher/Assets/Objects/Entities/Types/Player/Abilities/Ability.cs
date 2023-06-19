using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Ability<T> where T : MonoBehaviour
{
    protected T m_root = null;

    protected bool m_active = false;

    public bool active { get => m_active; }

    public Ability(T root)
    {
        m_root = root;
    }

    public virtual void ActivateAbility()
    {
        m_active = true;
    }

    public abstract void OnAbilityActive(float deltaTime);

    public abstract float GetCooldown();
}
