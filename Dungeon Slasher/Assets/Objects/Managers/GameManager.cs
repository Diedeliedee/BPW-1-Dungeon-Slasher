using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher
{
    public class GameManager : Singleton<GameManager>
    {
        public Agents.Management.AgentMananager agents;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            agents.Initialize();
        }

        private void Update()
        {
            agents.Tick(Time.deltaTime);
        }
    }
}
