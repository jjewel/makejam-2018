using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Attach 2 audiosources, 1 for sfx and another for music
public class AudioManager : MonoBehaviour {
	public static AudioManager instance;

	public AudioClip[] music, menuSFX;
	public AudioMixer mainMixer;//Used to hold a reference to the AudioMixer mainMixer
	public AudioMixerSnapshot musicDown, musicUp;
	[Range(0.5f,1f)]
	public float lowPitchRange = .95f;//The lowest a sound effect will be randomly pitched.
	[Range(1f,1.5f)]
	public float highPitchRange = 1.05f;//The highest a sound effect will be randomly pitched.

	public AudioSource musicSource, SFXSource;
	[Range(0.01f, 1f)]
	public float musicFadeIn = .01f;
	[Range(0.1f, 5f)]
	public float musicFadeOut =.1f;	


	void Awake () 
	{
		// Instance setup and clone failsafe
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		
		//Get a component reference to the AudioSource attached to the game object
		musicSource = GetComponent<AudioSource> ();
	}

//	void OnEnable()
//	{
//		SceneManager.sceneLoaded += SceneWasLoaded;
//	}
//
//	void OnDisable()
//	{
//		SceneManager.sceneLoaded -= SceneWasLoaded;
//	}
//
//	public void SceneWasLoaded(Scene scene, LoadSceneMode mode)
//	{
//		FadeDown (musicFadeOut);
//		Invoke ("SelectMusicByIndex", musicFadeOut);
//	}

	public void SelectMusicByIndex(int i)
	{
		if (music.Length >= i && i != null) {
			PlaySelectedMusic (music [i]);
		} else {
			Debug.Log ("Music clip array does not match build index length");
		}
	}
	
	//Used if running the game in a single scene, takes an integer music source allowing you to choose a clip by number and play.
	public void PlaySelectedMusic(AudioClip clipToPlay)
	{
        musicSource.clip = clipToPlay;
		musicSource.Play ();
		FadeUp (musicFadeIn);
	}

	//Call this function to very quickly fade up the volume of master mixer
	public void FadeUp(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot musicUp
		musicUp.TransitionTo (fadeTime);
	}

	//Call this function to fade the volume to silence over the length of fadeTime
	public void FadeDown(float fadeTime)
	{
		//call the TransitionTo function of the audioMixerSnapshot musicDown
		musicDown.TransitionTo (fadeTime);
	}

	//Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
	public void SetMusicLevel(float musicLvl)
	{
		mainMixer.SetFloat("musicVol", musicLvl);
	}

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
	}
}
