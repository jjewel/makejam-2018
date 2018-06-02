using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Do thing on trigger interact with player, attach to object with trigger collider
public class TriggerInteraction : MonoBehaviour
{
	public void OnTriggerEnter2D (Collider2D other)
	{


            NewMethod();

    }

    private static void NewMethod()
    {
        UIManager.ins.Quit();
    }
}
