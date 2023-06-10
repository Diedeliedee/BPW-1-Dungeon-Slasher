using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools.Structure;

public class GameManager : Singleton<GameManager>
{
    public AgentManager agents = null;
    public PlayerCamera camera = null;
    [Space]
    public EventManager events = null;
    [Space]
    public EnvironmentManager level = null;

    private TimeManager m_timeManager = new TimeManager();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        agents.Setup();
        camera.Setup();

        if (level == null)
        {
            Debug.LogError("No environment manager found in the game manager. Be sure to reference the environment script!");
            return;
        }
        level.Setup();

        events.onEnemyHit.AddListener(EnemyHitPause);
    }

    private void Update()
    {
        m_timeManager.Tick(Time.unscaledDeltaTime);

        agents.Tick(Time.deltaTime);
        camera.Tick(Time.unscaledDeltaTime);

        level.Tick(Time.deltaTime);
    }

    private void EnemyHitPause()
    {
        m_timeManager.StartHitPause(0.065f, 0f);
    }
}
