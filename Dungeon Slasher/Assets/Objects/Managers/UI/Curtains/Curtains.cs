using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;

public class Curtains : MonoBehaviour
{
	[SerializeField] private float m_curtainTime = 1f;
	[Space]
    [SerializeField] private Curtain m_leftCurtain;
    [SerializeField] private Curtain m_rightCurtain;

    public float curtainValue = 1f;
	public float curtainSpeed = 0f;

	public void Setup()
	{
		m_leftCurtain.Setup();
		m_rightCurtain.Setup();
		curtainSpeed = 1f / m_curtainTime;
	}

    public void SetCurtainValue(float value)
    {
		var activeObject = value < 1f;

        m_leftCurtain.SetState(value);
        m_rightCurtain.SetState(value);

		m_leftCurtain.gameObject.SetActive(activeObject);
		m_rightCurtain.gameObject.SetActive(activeObject);

		curtainValue = value;
    }

	public bool HasClosed(float deltaTime)
	{
		var hasClosed = false;

		if (curtainValue <= 0) return true;
		curtainValue -= (curtainSpeed * deltaTime);
		if (curtainValue <= 0)
		{
			hasClosed = true;
			curtainValue = 0;
		}
		SetCurtainValue(curtainValue);
		return hasClosed;
	}

	public bool HasOpened(float deltaTime)
	{
		var hasOpened = false;

		if (curtainValue >= 1) return true;
		curtainValue += (curtainSpeed * deltaTime);
		if (curtainValue >= 1)
		{
			hasOpened = true;
			curtainValue = 1;
		}
		SetCurtainValue(curtainValue);
		return hasOpened;
	}
}
