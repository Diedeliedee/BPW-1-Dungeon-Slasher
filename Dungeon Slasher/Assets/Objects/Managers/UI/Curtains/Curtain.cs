using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curtain : MonoBehaviour
{
    [SerializeField] private float m_closedXPos;
    [SerializeField] private float m_openXPos;
    [SerializeField] private AnimationCurve m_movementCurve;

    private float m_yPos = 0f;
    private RawImage m_rawImage = null;
    private RectTransform m_rectTransform = null;

    public void Setup()
    {
        //  Getting references.
        m_rawImage = GetComponent<RawImage>();
        m_rectTransform = GetComponent<RectTransform>();

        //  Setting properties.
        m_yPos = m_rectTransform.anchoredPosition.y;
        m_rectTransform.anchoredPosition = new Vector2(m_openXPos, m_yPos);
    }

    public void SetState(float state)
    {
        var perc = m_movementCurve.Evaluate(state);
        //var color = m_rawImage.color;

        //color.a = Mathf.Lerp(1f, 0f, perc);
        m_rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(m_closedXPos, m_openXPos, perc), m_yPos);
        //m_rawImage.color = color;
    }
}
