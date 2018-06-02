using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    //Player player;
    public CanvasGroup fadeOutImageCanvasGroup, titleCanvasGroup;
    [Range(0.1f, 5f)]
    public float titleFadeTime = 2f, screenFadeTime = 2f;

    // Use this for initialization
    void Awake ()
    {
        //player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            Quit();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneWasLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneWasLoaded;
    }

    // Quit
    public void Quit()
    {
        StartCoroutine(FadeOutCanvasGroup(titleFadeTime, titleCanvasGroup));
        StartCoroutine(ScreenFade(true, screenFadeTime));
        StartCoroutine(DelayQuit(screenFadeTime));
    }

    public IEnumerator DelayQuit(float delay)
    {
        yield return new WaitForSeconds(delay);
        //If we are running in a standalone build of the game
#if UNITY_STANDALONE
        //Quit the application
        Application.Quit();
#endif

        //If we are running in the editor
#if UNITY_EDITOR
        //Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // SCENE MANAGEMENT **********************************************************************************************
    void SceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ScreenFade(false, screenFadeTime));

        // Show cursor and set bool if loaded level is main menu
        if (scene.buildIndex == 0)
        {
            Cursor.visible = false;
            StartCoroutine(FadeInCanvasGroup(titleFadeTime, titleCanvasGroup));
        }
    }

    // Screen fade
    public IEnumerator ScreenFade(bool fadingOut, float fadeTime)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            if (fadingOut)
                fadeOutImageCanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            // Otherwise we're fading in
            else
                fadeOutImageCanvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            yield return null;
        }
    }

    // Disable and fade out canvasgroup
    public IEnumerator FadeOutCanvasGroup(float fadeTime, CanvasGroup canvasGroup)
    {
        canvasGroup.interactable = false;
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            canvasGroup.alpha = currentAlpha;
            yield return null;
        }

        canvasGroup.gameObject.SetActive(false);
    }

    // Fade in and enable canvasgroup
    public IEnumerator FadeInCanvasGroup(float fadeTime, CanvasGroup canvasGroup)
    {
        canvasGroup.gameObject.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            canvasGroup.alpha = currentAlpha;
            yield return null;
        }
        canvasGroup.interactable = true;
    }
}
