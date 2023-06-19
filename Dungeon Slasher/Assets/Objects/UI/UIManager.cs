using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Constants:")]
    [SerializeField] private PlayerHealthBar m_healthBar;

    [Header("Conditionals:")]
    [SerializeField] private Curtains m_curtains;
    [SerializeField] private GameOverScreen m_gameOverScreen;
    [SerializeField] private GameObject m_interactionIndicator;
    [SerializeField] private HealIndicator m_healIndicator;

    public void Setup()
    {
        m_healthBar.Setup();
        m_curtains.Setup();
        m_healIndicator.Setup();
    }

    public void Tick(float deltaTime)
    {
        m_healIndicator.Tick(deltaTime);
    }

    public void SetCurtainValue(float value)
    {
        m_curtains.SetCurtainValue(value);
    }

    public bool CurtainsClosed(float deltaTime)
    {
        return m_curtains.HasClosed(deltaTime);
    }

    public bool CurtainsOpened(float deltaTime)
    {
        return m_curtains.HasOpened(deltaTime);
    }

    public void DisplayInteractionButton(bool value)
    {
        m_interactionIndicator.SetActive(value);
    }

    public void ShowDeathScreen()
    {
        m_gameOverScreen.gameObject.SetActive(true);
        m_gameOverScreen.Display();
    }
}
