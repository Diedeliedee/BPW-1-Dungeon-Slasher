using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Joeri.Tools;

public partial class PlayerCamera : MonoBehaviour
{
    [Header("Camera Shake Options:")]
    [SerializeField] private ShakeInstance m_enemyHitShake;
    [SerializeField] private ShakeInstance m_playerHitShake;

    [Header("Reference")]
    [SerializeField] private Transform m_camera     = null;

    private ShakeInstancer m_cameraShake            = null;

    public void Setup()
    {
        m_cameraShake = new ShakeInstancer(m_camera.localPosition, 0f, 60f, 0f);
        GameManager.instance.events.onEnemyHit.AddListener(MildShake);
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
        m_enemyHitShake.Activate(this);
    }

    private void BigShake()
    {
        m_playerHitShake.Activate(this);
    }

    [System.Serializable]
    public class ShakeInstance
    {
        public float magnitude;
        public float duration;

        public void Activate(PlayerCamera root)
        {
            root.m_cameraShake.smoothening = duration;
            root.m_cameraShake.magnitude = magnitude;
        }
    }
}
