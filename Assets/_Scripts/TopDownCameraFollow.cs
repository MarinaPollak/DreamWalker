using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform player;          // Player reference
    public float yMultiplier = 0.5f;  // How much camera climbs per unit of X movement
    public float smoothSpeed = 5f;    // Smoothness of movement

    private float startY;             // camera's original y position
    private float startPlayerX;       // player's starting X

    void Start()
    {
        startY = transform.position.y;
        startPlayerX = player.position.x;
    }

    void LateUpdate()
    {
        // how far the player has moved on the X axis
        float xDistance = player.position.x - startPlayerX;

        // camera target height = base height + (player x distance * multiplier)
        float targetY = startY + (xDistance * yMultiplier);

        // clamp camera so it never goes below its starting y
        if (targetY < startY)
            targetY = startY;

        // apply movement smoothly
        Vector3 targetPos = new Vector3(
            transform.position.x,     // camera fixed in X
            targetY,                  // only y changes
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}
