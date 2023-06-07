using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Utilities;

public class Blockade : MonoBehaviour
{
    [SerializeField] private AnimationClip m_riseAnim;
    [SerializeField] private AnimationClip m_fallAnim;

    private Animator m_animator = null;

    public void Setup()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Rise()
    {
        m_animator.CrossFade(m_riseAnim.name, 0f);
    }

    public void Fall()
    {
        m_animator.CrossFade(m_fallAnim.name, 0f);
    }
}
