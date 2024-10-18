using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform player;  
    public Vector2 minBounds; 
    public Vector2 maxBounds; 
    public float smoothTime = 0.3f; 

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position;

            targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            smoothedPosition.z = transform.position.z;

            transform.position = smoothedPosition;
        }
    }
}
