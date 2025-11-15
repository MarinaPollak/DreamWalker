using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public static string NextSpawnID = "";

    void Start()
    {
        if (NextSpawnID == "") return;

        SpawnPoint[] points = FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None);
        foreach (var point in points)
        {
            if (point.spawnID == NextSpawnID)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = point.transform.position;
                break;
            }
        }

        NextSpawnID = ""; // reset
    }
}
