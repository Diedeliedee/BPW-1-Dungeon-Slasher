using UnityEngine;

namespace DungeonSlasher.Agents
{
    [CreateAssetMenu(fileName = "Player Settings", menuName = "ScriptableObjects/Agents/Player Settings", order = 1)]
    public class PlayerSettings : ScriptableObject
    {
        public Player.FreeMove.Settings freeMove = null;
        public Player.Attack.Settings attack = null;
    }
}
