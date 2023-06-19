using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("References:")]
    public AgentManager entities = null;
    public PlayerCamera camera = null;
    public UIManager ui = null;
    [Space]
    public EventManager events = null;
    [Space]
    public EnvironmentManager level = null;

    [Header("Properties:")]
    [SerializeField] private string m_winScene;
    [SerializeField] private float m_deathTimeDepletion = 10f;

    //  Properties:
    private State m_state = State.Intro;

    //  Sub-managers:
    private TimeManager m_timeManager = new TimeManager();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        entities.Setup();
        camera.Setup();
        ui.Setup();

        if (level == null)
        {
            Debug.LogError("No environment manager found in the game manager. Be sure to reference the environment script!");
            return;
        }
        level.Setup();

        events.onEnemyHit.AddListener(EnemyHitPause);
        events.onPlayerHit.AddListener(PlayerHitPause);

        entities.player.onDeath += OnLose;

        ui.SetCurtainValue(0f);
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        var unscaledDeltaTime = Time.unscaledDeltaTime;

        switch (m_state)
        {
            case State.Intro:
                if (ui.CurtainsOpened(unscaledDeltaTime))
                {
                    m_state = State.Running;
                }
                break;

            case State.Running:
                m_timeManager.Tick(unscaledDeltaTime);

                entities.Tick(deltaTime);
                camera.Tick(unscaledDeltaTime);
                ui.Tick(unscaledDeltaTime);

                level.Tick(deltaTime);
                break;

            case State.Won:
                m_timeManager.Tick(unscaledDeltaTime);
                camera.Tick(unscaledDeltaTime);
                if (ui.CurtainsClosed(unscaledDeltaTime))
                {
                    instance = null;
                    LoadScene(m_winScene);
                }
                break;

            case State.Died:
                camera.Tick(unscaledDeltaTime);
                break;
        }

    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1f;
        instance = null;
        SceneManager.LoadScene(name);
    }

    public void OnLose()
    {
        m_state = State.Died;
        ui.ShowDeathScreen();
    }

    public void OnWin()
    {
        m_state = State.Won;
    }

    private void EnemyHitPause(int health, int maxHealth)
    {
        m_timeManager.StartHitPause(0.065f, 0f);
    }

    private void PlayerHitPause(int health, int maxHealth)
    {
        m_timeManager.StartHitPause(0.3f, 0f);
    }

    public enum State { Intro, Running, Died, Won }
}
