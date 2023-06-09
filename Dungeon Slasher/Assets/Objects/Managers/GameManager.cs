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

    private TimeManager m_timeManager = new TimeManager();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        camera.Setup();

        events.onEnemyHit.AddListener(HitPause);
    }

    private void Update()
    {
        m_timeManager.Tick(Time.unscaledDeltaTime);

        agents.Tick(Time.deltaTime);
        camera.Tick(Time.unscaledDeltaTime);
    }

    private void HitPause()
    {
        m_timeManager.StartHitPause(0.0625f, 0.25f);
    }
}
