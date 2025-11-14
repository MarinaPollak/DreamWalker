using UnityEngine;

public class TopDownPlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // prevent physics tipping over
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D or Left/Right
        float vertical = Input.GetAxisRaw("Vertical");      // W/S or Up/Down

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // face movement direction
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRot = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Lerp(rb.rotation, targetRot, 0.15f);
        }
    }
}
