using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

        if (respawn != null)
        {
            respawn.currentSpawnPoint = transform;
        }
    }
}
