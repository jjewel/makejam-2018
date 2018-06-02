using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// Handles enabling the player on start, disabling the start screen GUI, enabling endgame GUI
public class GameManager : MonoBehaviour {
    int myPhase = -1;
    public Phase[] phases;
    Player player;
    Camera mainCamera;
    Color targetColour;

    bool fadingIn = false, fadingOut = false;
    [Header("Global Values")]
    public float colourFadeSpeed = 0.1f;
    public float audioFadeSpeed = 0.1f, minVolume = 0f, maxVolume = 1f;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        mainCamera = Camera.main;
        targetColour = phases[0].backgroundColour;
        mainCamera.backgroundColor = targetColour;
        foreach(Phase phase in phases)
        {
            phase.ambientAudio.volume = 0f;
        }
    }

    // Update phase
    void Update()
    {
        int phaseCheck = PhaseCheck();

        if (phaseCheck != myPhase)
        {
            PhaseChange(myPhase, phaseCheck);
            myPhase = phaseCheck;
        }

        mainCamera.backgroundColor = 
            Color.Lerp(mainCamera.backgroundColor, targetColour, colourFadeSpeed * Time.deltaTime);
    }

    // Check current phase
    public int PhaseCheck()
    {
        int currentPhase = -1;
        float playerDistance = player.transform.position.magnitude;

        foreach (Phase phase in phases)
        {
            if (phase.startDistance < playerDistance)
                ++currentPhase;
        }

        return currentPhase;
    }

    // Move background colour and audio
    public void PhaseChange(int from, int to)
    {
        StartCoroutine(FadeDown(phases[from].ambientAudio, audioFadeSpeed, minVolume));
        StartCoroutine(FadeUp(phases[to].ambientAudio, audioFadeSpeed, maxVolume));
        targetColour = phases[to].backgroundColour;
    }

    //Call this function to very quickly fade up the volume of master mixer
    public IEnumerator FadeUp (AudioSource _source, float _fadeSpeed, float _maxVolume)
    {
        fadingIn = true;
        fadingOut = false;
        _source.volume = 0;
        float targetVolume = 0f;
        while(_source.volume < _maxVolume && fadingIn)
        {
            targetVolume += _fadeSpeed;
            _source.volume = targetVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    //Call this function to fade the volume to silence over the length of fadeTime
    public IEnumerator FadeDown(AudioSource _source, float _fadeSpeed, float _minVolume)
    {
        fadingIn = false;
        fadingOut = true;
        _source.volume = 0;
        float targetVolume = 0f;
        while (_source.volume > _minVolume && fadingOut)
        {
            targetVolume += _fadeSpeed;
            _source.volume = targetVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

[Serializable]
public class Phase
{
    public float startDistance;
    public Color backgroundColour = Color.magenta;
    public AudioSource ambientAudio;
    public float maxVolume = 1f;
}

