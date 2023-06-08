using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Joeri.Tools;

public class TimeManager
{
    private Timer m_hitPauseTimer = null;

    public void Tick(float deltaTime)
    {
        if (m_hitPauseTimer == null) return;
        if (m_hitPauseTimer.HasReached(deltaTime))
        {
            Time.timeScale = 1f;
            m_hitPauseTimer = null;
        }
    }

    public void StartHitPause(float time, float timeScale)
    {
        if (m_hitPauseTimer != null) return;

        m_hitPauseTimer = new Timer(time);
        Time.timeScale = timeScale;
    }
}
