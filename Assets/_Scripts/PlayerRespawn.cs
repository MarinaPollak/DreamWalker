using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform currentSpawnPoint;
    public float respawnDelay = 1f;

    private TopDownPlayerMovement movementScript;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        movementScript = GetComponent<TopDownPlayerMovement>();
    }

    public void Respawn()
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        movementScript.enabled = false;
        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(respawnDelay);

        // teleport player
        rb.position = currentSpawnPoint.position;

        movementScript.enabled = true;
    }
}
