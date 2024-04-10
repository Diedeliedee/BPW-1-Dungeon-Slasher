using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeAnnouncement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_descriptionText;

    private Animator m_animator = null;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void OnAbilityAdded(string _name, string _description)
    {
        m_descriptionText.text = _description;
        m_animator.Play("Reveal");
    }
}
