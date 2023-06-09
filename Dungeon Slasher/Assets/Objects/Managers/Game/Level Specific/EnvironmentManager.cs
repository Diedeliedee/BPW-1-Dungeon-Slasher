using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    //[SerializeField] private Arena[] m_arenas;
    //[Space]
    [SerializeField] private EnemySpawner[] m_looseSpawners;

    public void Setup()
    {
        //for (int i = 0; i < m_arenas.Length; i++) m_arenas[i].Setup();
        for (int i = 0; i < m_looseSpawners.Length; i++) m_looseSpawners[i].Setup();
    }

    public void Tick(float deltaTime)
    {

    }
}
