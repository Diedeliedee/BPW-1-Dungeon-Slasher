using Joeri.Tools.Utilities;
using UnityEngine;
using System.Collections;
using UnityEditor.ShaderGraph.Internal;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float m_stopScale = 0.1f;

    private Coroutine m_currentRoutine = null;

    private bool m_stopped = false;
    private IEnumerator m_followBuffer = null;

    public void StopFor(float _time)
    {
        if (m_currentRoutine != null)
        {
            StopCoroutine(m_currentRoutine);
        }

        
        Time.timeScale = m_stopScale;
        m_stopped = true;
        m_currentRoutine = StartCoroutine(Routines.WaitForSecondsRealtime(_time, OnFinish));

        void OnFinish()
        {
            if (m_followBuffer != null)
            {
                m_currentRoutine = StartCoroutine(m_followBuffer);
                m_followBuffer = null;
            }

            Time.timeScale = 1f;
            m_stopped = false;
        }
    }

    public void Follow(float _time, AnimationCurve _curve)
    {
        var enumerator = Routines.CustomProgressionRealtime(_time, _curve, OnTick, OnFinish);

        if (m_stopped)  m_followBuffer = enumerator;
        else            m_currentRoutine = StartCoroutine(enumerator);

        void OnTick(float _progress)
        {
            Time.timeScale = _progress;
        }

        void OnFinish()
        {
            m_currentRoutine = null;
        }
    }
}
