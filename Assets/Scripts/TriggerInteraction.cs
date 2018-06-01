using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Load scene on trigger
public class TriggerInteraction : MonoBehaviour
{
	public int loadSceneIndex = 1;
	public float sceneFadeTime = 2f;

	public void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			MenuManager.instance.StartFadeToScene (loadSceneIndex, sceneFadeTime);
		}
	}
}
