using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Joeri.Tools;

public class HealIndicator : MonoBehaviour
{
    [SerializeField] private float m_time;
    [SerializeField] private float m_maxAlpha;

    private RawImage m_image = null;
    private AudioSource m_audio = null;

    private Timer m_timer = null;
    private bool m_active = false;

    public void Setup()
    {
        m_timer = new Timer(m_time);
        m_image = GetComponent<RawImage>();
        m_audio = GetComponent<AudioSource>();
        GameManager.instance.events.onPlayerHeal.AddListener(Ding);
    }    

    public void Tick(float deltaTime)
    {
        if (!m_active) return;

        SetAlpha(m_timer.percent * -1 + 1);
        if (m_timer.ResetOnReach(deltaTime))
        {
            m_active = false;
            SetAlpha(0f);
        }
    }


    public void Ding()
    {
        m_active = true;
        SetAlpha(1f);
        m_audio.Play();
        m_timer.Reset();
    }

    private void SetAlpha(float a)
    {
        var color = m_image.color;
        var setEnabled = a > 0f;

        color.a = Mathf.Lerp(0f, m_maxAlpha, a);
        m_image.color = color;
        m_image.gameObject.SetActive(setEnabled);
    }
}
