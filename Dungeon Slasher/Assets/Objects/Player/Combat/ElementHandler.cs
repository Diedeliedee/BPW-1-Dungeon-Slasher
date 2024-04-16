using Joeri.Tools.Patterns;
using UnityEngine;
using UnityEngine.Events;

public class ElementManager : MonoBehaviour
{
    [SerializeField] private bool m_allowManualSwitch   = true;
    [SerializeField] private Element m_element          = Element.Light;
    [Space]
    [SerializeField] private UnityEvent m_onSwitchToLight;
    [SerializeField] private UnityEvent m_onSwitchToDark;

    private InputReader m_input     = null;
    private PlayerWeapon m_weapon   = null;

    private void Awake()
    {
        m_input     = ServiceLocator.instance.Get<InputReader>("Input Reader");
        m_weapon    = GetComponentInChildren<PlayerWeapon>();
    }

    private void Start()
    {
        SwitchTo(m_element);
    }

    private void Update()
    {
        //  If manual switching is allowed, switch the element as soon as the button has been pressed.
        if (!m_allowManualSwitch || !m_input.switchInput) return;
        Switch();
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
        m_weapon.SwitchTo(_element);
    }
}
