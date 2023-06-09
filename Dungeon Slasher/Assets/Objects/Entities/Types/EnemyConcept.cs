using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Concepts/Enemies", order = 0)]
public class EnemyConcept : ScriptableObject
{
    [SerializeField] private Enemy.Type m_type;
    [SerializeField] private GameObject m_prefab;
    [Space]
    [SerializeField] private float m_indicatorRadius = 0.3f;
    [SerializeField] private Color m_indicatorColor;

    public Enemy.Type type { get => m_type; }
    public GameObject prefab { get => m_prefab; }

    public float radius { get => m_indicatorRadius; }
    public Color color { get => m_indicatorColor; }
}