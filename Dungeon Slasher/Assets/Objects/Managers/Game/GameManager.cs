using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    public AgentManager entities = null;
    public PlayerCamera camera = null;
    public UIManager ui = null;
    [Space]
    public EventManager events = null;
    [Space]
    public EnvironmentManager level = null;

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
    }

    private void Update()
    {
        var deltaTime = Time.deltaTime;
        var unscaledDeltaTime = Time.unscaledDeltaTime;

        switch (m_state)
        {
            case State.Intro:
                if (ui.CurtainsOpened(del))

            case State.Running:
                m_timeManager.Tick(Time.unscaledDeltaTime);

                entities.Tick(Time.deltaTime);
                camera.Tick(Time.unscaledDeltaTime);
                ui.Tick(Time.unscaledDeltaTime);

                level.Tick(Time.deltaTime);
                break;
        }
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
