using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance { get; private set; }
    [Header("Optional Fade")]
    [SerializeField] private CanvasGroup fadeCanvas;   // taruh UI full-screen hitam (alpha 0..1)
    [SerializeField] private float fadeDuration = 0.3f;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadWithDelay(string sceneName, float delaySeconds = 0f)
    {
        StartCoroutine(CoLoadWithDelay(sceneName, delaySeconds));
    }

    public void LoadWithFade(string sceneName)
    {
        StartCoroutine(CoLoadWithFade(sceneName));
    }

    private IEnumerator CoLoadWithDelay(string sceneName, float delay)
    {
        if (delay > 0f) yield return new WaitForSeconds(delay);
        SceneLoader.LoadSceneByName(sceneName);
    }

    private IEnumerator CoLoadWithFade(string sceneName)
    {
        // Fade out
        if (fadeCanvas) yield return StartCoroutine(Fade(1f));
        var op = SceneLoader.LoadSceneByName(sceneName);
        if (op != null) while (!op.isDone) yield return null;
        // Fade in
        if (fadeCanvas) yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float target)
    {
        float start = fadeCanvas.alpha;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(start, target, t / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = target;
    }
}
