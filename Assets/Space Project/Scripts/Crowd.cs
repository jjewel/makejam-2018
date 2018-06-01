using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {
    public float crowdSpeed;
    public float newCrowdSpeed;
    public float newCrowdSpeedMax;
    public float newCrowdSpeedMin;

    public float randomDirectionChange;

    public float pauseMovement;
   

	// Use this for initialization
	void Start () {
        InvokeRepeating("chooseDirection", 0f, randomDirectionChange);
    }

    void chooseDirection()
    {
        crowdSpeed = 0f;

        newCrowdSpeed = Random.Range(newCrowdSpeedMin, newCrowdSpeedMax);

            //code to randomnly choose a direction. Sets the new randomDirectionChange value to a range between randomDirectionChangeMax and Min. Chooses new Crowdspeed based on max and min range.

        Invoke("Move", pauseMovement);
    }

    void Move()
    {
        crowdSpeed = newCrowdSpeed;
    }

}
