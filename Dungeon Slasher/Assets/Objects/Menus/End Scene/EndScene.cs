using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [Header("Properties:")]
    [SerializeField] private string m_mainMenuScene;

    [Header("Menu Components:")]
    [SerializeField] private Curtains m_curtains;

    private State m_state = State.Idle;

    private void Start()
    {
        m_curtains.Setup();
        m_curtains.SetCurtainValue(0f);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;

        switch (m_state)
        {
            case State.Idle:
                m_curtains.HasOpened(deltaTime);
                break;

            case State.Returning:
                if (!m_curtains.HasClosed(deltaTime)) break;
                SceneManager.LoadScene(m_mainMenuScene);
                break;

            case State.Quitting:
                if (!m_curtains.HasClosed(deltaTime)) break;
                Application.Quit();
                break;
        }
    }

    public void MainMenu()
    {
        if (m_state != State.Idle) return;
        m_state = State.Returning;
    }

    public void QuitGame()
    {
        if (m_state != State.Idle) return;
        m_state = State.Quitting;
    }

    private enum State { Idle, Returning, Quitting }
}
