using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher
{
    public class GameManager : Singleton<GameManager>
    {
        public Agents.Management.AgentManager agents    = null;
        public PlayerCamera camera                      = null;
        [Space]
        public EventManager events                      = null;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            agents.Initialize();
            camera.Initialize();

            events.onEnemyHit.AddListener(HitPause);
        }

        private void Update()
        {
            agents.Tick(Time.deltaTime);
            camera.Tick(Time.unscaledDeltaTime);
        }

        private void HitPause()
        {
            Time.timeScale = 0;

            void OnFinish()
            {
                Time.timeScale = 1f;
            }

            StartCoroutine(CommonRoutines.WaitForSecondsRealtime(0.1f, OnFinish));
        }
    }
}
