using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 1f;
	
	// Update is called once per frame
	void Update () {

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Look the intended movement direction
        if (x > 0.1f)
        {
           // flip spritex false

        }
        else if (x < -0.1f)
        {
            // flip spritex true
        }

        transform.Translate(new Vector3(x, y, 0).normalized * speed * Time.deltaTime);
    }
}
