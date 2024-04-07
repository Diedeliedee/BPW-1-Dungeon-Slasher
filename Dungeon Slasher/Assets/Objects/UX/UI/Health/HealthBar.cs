using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private HealthPoint[] m_healthPoints = null;

    private void Awake()
    {
        m_healthPoints = GetComponentsInChildren<HealthPoint>();
    }

    public void OnHealthChange(int _health, int _maxHealth)
    {
        for (int i = 0; i < m_healthPoints.Length; i++)
        {
            //  If the current health point index is below the current health count. Fill it.
            if (_health > i)    m_healthPoints[i].Fill();

            //  Otherwise, deplete the health point.
            else                m_healthPoints[i].Deplete();
        }
    }
}
