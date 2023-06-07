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

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        agents.Setup();
        camera.Setup();

        events.onEnemyHit.AddListener(HitPause);
    }

    private void Update()
    {
        agents.Tick(Time.deltaTime);
        camera.Tick(Time.unscaledDeltaTime);
    }

    private void HitPause()
    {
        Time.timeScale = 0.1f;

        void OnFinish()
        {
            Time.timeScale = 1f;
        }

        StartCoroutine(CommonRoutines.WaitForSecondsRealtime(0.1f, OnFinish));
    }
}
