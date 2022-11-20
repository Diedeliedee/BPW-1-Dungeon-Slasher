using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DungeonSlasher.Agents
{
    class Control : Behavior
    {
        public override Vector2 GetDesiredVelocity(Vector2 position, float speed)
        {
            return Player.Controls.leftInput.normalized * speed;
        }
    }
}
