using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform target;         // The player
    public float smoothSpeed = 5f;
    public float radius = 0.4f;      // spherecast radius
    public float collisionOffset = 0.3f;
    public LayerMask collisionMask;  // assign walls/geometry

    private Vector3 desiredOffset;

    void Start()
    {
        desiredOffset = transform.position - target.position;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculate the ideal camera position
        Vector3 desiredPos = target.position + desiredOffset;

        // Direction FROM player TO camera
        Vector3 direction = (desiredPos - target.position).normalized;
        float distance = Vector3.Distance(target.position, desiredPos);

        RaycastHit hit;

        // Check if something is in the way
        if (Physics.SphereCast(target.position, radius, direction, out hit, distance, collisionMask))
        {
            // Move camera closer to the target to avoid clipping
            Vector3 adjustedPos = hit.point - direction * collisionOffset;

            transform.position = Vector3.Lerp(
                transform.position,
                adjustedPos,
                smoothSpeed * Time.deltaTime
            );
        }
        else
        {
            // No collision — go to normal position
            transform.position = Vector3.Lerp(
                transform.position,
                desiredPos,
                smoothSpeed * Time.deltaTime
            );
        }

        // Keep rotation fixed (top-down)
        // or keep your camera’s inspector rotation
    }
}
