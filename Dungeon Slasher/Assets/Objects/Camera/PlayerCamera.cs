using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;
using DungeonSlasher;

public partial class PlayerCamera : MonoBehaviour
{
    [Header("Camera Shake Options:")]
    [SerializeField] private float m_mildMagnitude  = 0.25f;
    [SerializeField] private float m_bigMagnitude   = 1f;
    [SerializeField] private float m_recovery       = 0.1f;

    [Header("Reference")]
    [SerializeField] private Transform m_camera     = null;

    private ShakeInstancer m_cameraShake            = null;

    public void Initialize()
    {
        m_cameraShake = new ShakeInstancer(m_camera.localPosition, 0f, 60f, m_recovery);
        //GameManager.instance.events.onEnemyHit.AddListener(MildShake);
        GameManager.instance.events.onPlayerHit.AddListener(BigShake);

        SetOffset();
    }

    public void Tick(float deltaTime)
    {
        FollowPlayer();
        m_camera.localPosition = m_cameraShake.GetPosition(deltaTime);
    }

    private void MildShake()
    {
        m_cameraShake.magnitude = m_mildMagnitude;
        m_cameraShake.smoothening = 0.05f;
    }

    private void BigShake()
    {
        m_cameraShake.magnitude = m_bigMagnitude;
        m_cameraShake.smoothening = 0.1f;
    }
}
