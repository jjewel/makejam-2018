using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd : MonoBehaviour {
    public float crowdSpeed;
    public float newCrowdSpeed;
    public float newCrowdSpeedMax;
    public float newCrowdSpeedMin;

    public float RandomX;
    private float RandomY;
    private float RandomZ;

    private float pauseMovement;
    public float pauseMovementMin;
    public float pauseMovementMax;

    private Animator animate;

    private float walkTime;
    public float walkTimeMin;
    public float walkTimeMax;

    private float startX;

	// Use this for initialization
	void Start () {
        startX = transform.localScale.x;
        animate = GetComponent<Animator>();
        Invoke("chooseDirection", pauseMovement);
    }

    void Update()
    {
        transform.Translate(RandomX * crowdSpeed, RandomY * crowdSpeed, RandomZ * crowdSpeed);
    }

    void chooseDirection()
    {
        animate.SetBool("Walking", false);
        crowdSpeed = 0f;

        newCrowdSpeed = Random.Range(newCrowdSpeedMin, newCrowdSpeedMax);

        RandomX = Random.Range(-1f, 1f);
        RandomY = Random.Range(-1f, 1f);

        pauseMovement = Random.Range(pauseMovementMin, pauseMovementMax);

        Invoke("Move", pauseMovement);

        //code to randomnly choose a direction. Sets the new randomDirectionChange value to a range between randomDirectionChangeMax and Min. Chooses new Crowdspeed based on max and min range.
    }

    void Move()
    {
        if (RandomX < 0f)
        {
            transform.localScale = new Vector3(-startX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(startX, transform.localScale.y, transform.localScale.z);
        }

        animate.SetBool("Walking", true);

        crowdSpeed = newCrowdSpeed;
        walkTime = Random.Range(walkTimeMin, walkTimeMax);
        Invoke("chooseDirection", walkTime);
    }

}
