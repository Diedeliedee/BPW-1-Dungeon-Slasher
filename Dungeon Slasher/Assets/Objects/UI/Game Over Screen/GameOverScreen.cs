using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Joeri.Tools;
using Joeri.Tools.Utilities;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private AudioSource m_audio = null;
    [SerializeField] private RawImage m_filter = null;
    [Space]
    [SerializeField] private float m_time = 1f;

    private Timer m_filterTimer = null;
    private bool m_active = false;

    public void Display()
    {
        gameObject.SetActive(true);
        m_audio.Play();
        m_filterTimer = new Timer(m_time);
        m_active = true;
    }

    public void Retry()
    {
        GameManager.instance.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (!m_active) return;

        var color = m_filter.color;

        if (m_filterTimer.HasReached(Time.unscaledDeltaTime))
        {
            m_active = false;
            m_filter.gameObject.SetActive(false);
        }

        color.a = Mathf.Lerp(1f, 0f, m_filterTimer.percent);
        m_filter.color = color;
    }
}
