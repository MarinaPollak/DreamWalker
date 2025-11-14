using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator FadeOut()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }

    public IEnumerator FadeIn()
    {
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, a);
            yield return null;
        }
    }
}
