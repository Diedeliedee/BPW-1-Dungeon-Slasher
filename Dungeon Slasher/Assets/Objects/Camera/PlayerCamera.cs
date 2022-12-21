using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dodelie.Tools;
using DungeonSlasher;

public partial class PlayerCamera : MonoBehaviour
{
    [Header("Camera Shake Options:")]
    [SerializeField] private float m_mildMagnitude  = 0.25f;
    [SerializeField] private float m_bigMagnitude   = 1f;

    [Header("Reference")]
    [SerializeField] private Transform m_camera     = null;

    private ShakeInstancer m_cameraShake            = null;

    public void Initialize()
    {
        m_cameraShake = new ShakeInstancer(m_camera.localPosition, 0f, 60f, 0.1f);
        GameManager.instance.events.onEnemyHit.AddListener(MildShake);
        GameManager.instance.events.onPlayerHit.AddListener(BigShake);

        SetOffset();
    }

    public void Tick(float deltaTime)
    {
        FollowPlayer();
        m_camera.localPosition = m_cameraShake.GetOffset(deltaTime);
    }

    private void MildShake()
    {
        m_cameraShake.magnitude = m_mildMagnitude;
    }

    private void BigShake()
    {
        m_cameraShake.magnitude = m_bigMagnitude;
    }
}
