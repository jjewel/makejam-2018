using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float currentSpeed;

    private float startX;

    private Animator animate;

    // Use this for initialization
    void Start () {
        startX = transform.localScale.x;
        animate = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * currentSpeed);
            transform.localScale = new Vector3(-startX, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * currentSpeed);
            transform.localScale = new Vector3(startX, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * currentSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * currentSpeed);
        }

        if (Input.anyKey)
        {
            animate.SetBool("Walking", true);
        }
        else
        {
            animate.SetBool("Walking", false);
        }

    }
}
