using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    [SerializeField] private Arena[] m_arenas;
    [Space]
    [SerializeField] private EnemySpawner[] m_looseSpawners;

    public void Setup()
    {
        foreach (var arena in m_arenas) arena.Setup();
        foreach (var spawner in m_looseSpawners) spawner.Setup();
    }

    public void Tick(float deltaTime)
    {
        for (int i = 0; i < m_arenas.Length; i++)
        {
            m_arenas[i].Tick(deltaTime);
        }
    }
}
