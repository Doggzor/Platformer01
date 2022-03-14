using UnityEngine;

public class VirtualCameraPositionController : MonoBehaviour
{
    public void StopPositionUpdates()
    {
        transform.position = Camera.main.transform.position;
    }
}
