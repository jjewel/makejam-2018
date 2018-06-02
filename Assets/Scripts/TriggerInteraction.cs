using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Do thing on trigger interact with player, attach to object with trigger collider
public class TriggerInteraction : MonoBehaviour
{
	public void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			// Do the thing
            // eg. GetComponent<Animator>().SetTrigger("PlayMyAnimation");
            //MenuManager.instance.StartFadeToScene (loadSceneIndex, sceneFadeTime);
		}
	}
}
