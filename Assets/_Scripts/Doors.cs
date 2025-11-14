using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : MonoBehaviour
{
    [Header("Where this door sends the player")]
    public string targetSceneName;
    public string spawnPointID;

    private bool isTransitioning = false;

    private void OnTriggerEnter(Collider other)   // <-- 3D physics
    {
        if (isTransitioning) return;
        if (!other.CompareTag("Player")) return;

        isTransitioning = true;
        StartCoroutine(HandleTransition(other.gameObject));
    }

    private IEnumerator HandleTransition(GameObject player)
    {
        // Fade out
        yield return StartCoroutine(SceneFader.Instance.FadeOut());

        // Store where we want to spawn in next scene
        PlayerSpawnManager.NextSpawnID = spawnPointID;

        // Load next scene
        SceneManager.LoadScene(targetSceneName);

        // Wait a frame for the new scene to load
        yield return null;

        // Fade in
        yield return StartCoroutine(SceneFader.Instance.FadeIn());
    }
}
