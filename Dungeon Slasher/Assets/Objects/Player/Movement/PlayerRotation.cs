using Joeri.Tools.Utilities;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private const float m_offsetAngle = 45f;

    public void RotateTo(Vector2 _input)
    {
        _input.ApplyRotation(m_offsetAngle);

        var currentRotation = transform.rotation;
        var targetRotation = Quaternion.LookRotation(_input.Cubular(), Vector3.up);

        transform.rotation = targetRotation;
    }
}
