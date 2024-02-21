using UnityEngine;

public class DeleteOnAnimationEvent : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
