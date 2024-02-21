using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementManager : MonoBehaviour
{
    [SerializeField] private Element m_element = Element.Light;
    [Space]
    [SerializeField] private UnityEvent m_onSwitchToLight;
    [SerializeField] private UnityEvent m_onSwitchToDark;

    private void Start()
    {
        //SwitchTo(m_element);
    }

    public void Switch()
    {
        switch (m_element)
        {
            case Element.Light: SwitchTo(Element.Dark); break;
            case Element.Dark: SwitchTo(Element.Light); break;
        }
    }

    public void SwitchTo(Element _element)
    {
        m_element = _element;
        switch (_element)
        {
            case Element.Light: m_onSwitchToLight.Invoke(); break;
            case Element.Dark: m_onSwitchToDark.Invoke(); break;
        }
    }
}
