using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools.Utilities;

public partial class AgentManager
{
    [System.Serializable]
    public class Spawning
    {
        [SerializeField] private GameObject npcToSpawn;
        [SerializeField] private Transform m_entitiesTransform;

        public NPC SpawnEnemy(Vector3 pos, Vector2 lookDir, ref List<NPC> listToAddTo)
        {
            var angle = Vectors.VectorToAngle(lookDir);
            var rotation = Quaternion.Euler(0f, angle, 0f);
            var spawnedObject = Object.Instantiate(npcToSpawn, pos, rotation, m_entitiesTransform).GetComponent<NPC>();

            return spawnedObject;
        }
    }
}
