using Joeri.Tools.Utilities;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public void RotateTo(Vector2 _input)
    {
        var currentRotation = transform.rotation;
        var targetRotation = Quaternion.LookRotation(_input.Cubular(), Vector3.up);

        transform.rotation = targetRotation;
    }
}
