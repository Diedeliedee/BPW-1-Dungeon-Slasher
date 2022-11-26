using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;

namespace DungeonSlasher.Agents
{
    public partial class Player
    {
        [System.Serializable]
        public class LeftAttack : AttackState
        {
            [Space]
            public PlayerControls controls = null;

        }
    }
}