using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson; //Use to disable fps controller in pause menu

// Handles menu transitions and scene changes on button presses
public class MenuManager : MonoBehaviour
{
	public static MenuManager instance;

	public MenuSettings menuSettingsData;
	public bool changeScenesOnStart, changeMusicOnStart;
	private bool inMainMenu = true, inPauseMenu = false;
									
	private ShowPanels showPanels;
	public CanvasGroup fadeOutImageCanvasGroup, menuCanvasGroup, pauseCanvasGroup, optionsCanvasGroup;
	[Range (0.1f, 5f)]
	public float menuFadeTime = 1f, screenFadeTime = 2f, pauseFadeTime = .2f;

	void Awake ()
	{
		// Instance setup and clone failsafe
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);

		showPanels = GetComponent<ShowPanels> ();
	}

	// Update is called once per frame
	void Update ()
	{
		//Check if the Cancel button in Input Manager is down this frame (default is Escape key) 
		//and that game is not paused, and that we're not in main menu
		if (Input.GetButtonDown ("Cancel") && !inPauseMenu && !inMainMenu) {
			Debug.Log ("pausing");
			DoPause ();
		} 

		//If the button is pressed and the game is paused and not in main menu
		else if (Input.GetButtonDown ("Cancel") && inPauseMenu && inMainMenu) {
			UnPause ();
		}
	}

	void OnEnable ()
	{
		SceneManager.sceneLoaded += SceneWasLoaded;
	}

	void OnDisable ()
	{
		SceneManager.sceneLoaded -= SceneWasLoaded;
	}

	// BUTTONS *********************************************************************
		
	public void StartGameButton ()
	{
		//If changeScenesOnStart is true, start fading and change scenes after fade
		if (changeScenesOnStart) {
			StartCoroutine (FadeToNewScene (1, screenFadeTime));
			StartCoroutine (FadeOutCanvasGroup (menuFadeTime, menuCanvasGroup));
		} else {
			//Call the StartGameInScene function to start game without loading a new scene
			StartCoroutine (StartGameInScene ());
		}
	}

	// Quit button
	public void Quit ()
	{
		StartCoroutine (FadeOutCanvasGroup (menuFadeTime, menuCanvasGroup));
		StartCoroutine (ScreenFade (true, screenFadeTime));
		StartCoroutine (DelayQuit (screenFadeTime));
	}

	public IEnumerator DelayQuit(float delay)
	{
		yield return new WaitForSeconds(delay);
		//If we are running in a standalone build of the game
		#if UNITY_STANDALONE
		//Quit the application
		Application.Quit ();
		#endif

		//If we are running in the editor
		#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	// SCENE MANAGEMENT **********************************************************************************************
	void SceneWasLoaded (Scene scene, LoadSceneMode mode)
	{
		StartCoroutine (ScreenFade (false, screenFadeTime));

		// Show cursor and set bool if loaded level is main menu
		if (scene.buildIndex == 0) {
			inMainMenu = true;
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.Confined;
			StartCoroutine (FadeInCanvasGroup (menuFadeTime, menuCanvasGroup));
		} else {
			inMainMenu = false;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	// Screen fade
	public IEnumerator ScreenFade (bool fadingOut, float fadeTime)
	{
		float elapsedTime = 0f;

		while (elapsedTime < fadeTime) {
			elapsedTime += Time.deltaTime;
			if (fadingOut)
				fadeOutImageCanvasGroup.alpha = Mathf.Lerp (0, 1, elapsedTime / fadeTime);
			// Otherwise we're fading in
			else
				fadeOutImageCanvasGroup.alpha = Mathf.Lerp (1, 0, elapsedTime / fadeTime);
			yield return null;
		}
	}
	public void StartFadeToScene(int sceneIndex, float fadeTime)
	{
		StartCoroutine(FadeToNewScene(sceneIndex,fadeTime));
	}

	// Screen fade to chosen scene
	public IEnumerator FadeToNewScene (int sceneIndex, float fadeTime)
	{
		AudioManager.instance.FadeDown(fadeTime);
		StartCoroutine (ScreenFade (true, fadeTime));
		yield return new WaitForSeconds (fadeTime);
		AudioManager.instance.SelectMusicByIndex(sceneIndex);
		Debug.Log ("Loading scene...");
		//Load the selected scene, by scene index number in build settings
		SceneManager.LoadScene (sceneIndex);
	}

	// For if you want to start game in the same scene as main menu
	public IEnumerator StartGameInScene ()
	{
		StartCoroutine (FadeOutCanvasGroup (menuFadeTime, menuCanvasGroup));

		//If you want to change music, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic 
		if (changeMusicOnStart == true) {
			AudioManager.instance.SelectMusicByIndex (1);
		}

		// Wait to set inMainMenu to false
		yield return new WaitForSeconds (menuFadeTime * 2f);
		inMainMenu = false;
	}
		
	// Disable and fade out canvasgroup
	public IEnumerator FadeOutCanvasGroup (float fadeTime, CanvasGroup canvasGroup)
	{
		canvasGroup.interactable = false;
		float elapsedTime = 0f;

		while (elapsedTime < fadeTime) {
			elapsedTime += Time.deltaTime;
			float currentAlpha = Mathf.Lerp (1f, 0f, elapsedTime / fadeTime);
			canvasGroup.alpha = currentAlpha;
			yield return null;
		}

		canvasGroup.gameObject.SetActive (false);
	}

	// Fade in and enable canvasgroup
	public IEnumerator FadeInCanvasGroup (float fadeTime, CanvasGroup canvasGroup)
	{
		canvasGroup.gameObject.SetActive (true);
		float elapsedTime = 0f;

		while (elapsedTime < fadeTime) {
			elapsedTime += Time.deltaTime;
			float currentAlpha = Mathf.Lerp (0f, 1f, elapsedTime / fadeTime);
			canvasGroup.alpha = currentAlpha;
			yield return null;
		}
		canvasGroup.interactable = true;
	}
		
	// PAUSE MENU STUFF ************************************************************************************************

	public void DoPause ()
	{
		inPauseMenu = true;
		//Time.timeScale = 0;
		StartCoroutine (FadeInCanvasGroup (pauseFadeTime, pauseCanvasGroup));

		// Disable FPS control
		FirstPersonController.instance.enabled = false;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.Confined;
	}

	public void UnPause ()
	{
		inPauseMenu = false;
		//Time.timeScale = 1;
		StartCoroutine (FadeOutCanvasGroup (pauseFadeTime, pauseCanvasGroup));

		if (!inMainMenu) {
			FirstPersonController.instance.enabled = true;
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	public void ReturnToMenuButton ()
	{
		//Time.timeScale = 1;
		StartCoroutine (FadeOutCanvasGroup (menuFadeTime, pauseCanvasGroup));
		FirstPersonController.instance.enabled = false;
		inPauseMenu = false;
		StartCoroutine (FadeToNewScene (0, screenFadeTime));
	}
}
