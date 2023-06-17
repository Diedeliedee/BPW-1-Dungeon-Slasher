using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerHealthBar m_healthBar;
    [SerializeField] private Curtains m_curtains;

    public void Setup()
    {
        m_healthBar.Setup();
        m_curtains.Setup();
    }

    public void Tick(float deltaTime)
    {

    }

    public bool CurtainsClosed(float deltaTime)
    {
        return m_curtains.HasClosed(deltaTime);
    }

    public bool CurtainsOpened(float deltaTime)
    {
        return m_curtains.HasOpened(deltaTime);
    }
}
