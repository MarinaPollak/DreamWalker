using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerRespawn respawn = other.GetComponent<PlayerRespawn>();

        if (respawn != null)
        {
            respawn.Respawn();
        }
    }
}
