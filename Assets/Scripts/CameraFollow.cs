using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform transformToFollow;
    [SerializeField]
    float smoothTime = 0.25f;

    Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (transformToFollow)
        {
            Vector3 targetPosition = new Vector3(transformToFollow.position.x, transformToFollow.position.y, this.transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}