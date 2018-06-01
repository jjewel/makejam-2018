using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Invert toggle button behaviour
public class Invert : MonoBehaviour {
	public Toggle invertToggle;

	void Awake()
	{
		if (PlayerPrefs.GetInt ("Inverted") == 1)
			invertToggle.isOn = true;
		else
			invertToggle.isOn = false;
	}

	public void InvertMouseLook(bool inverted)
	{
		if (inverted)
			PlayerPrefs.SetInt ("Inverted", 1);
		else
			PlayerPrefs.SetInt("Inverted",0);
	}
}
