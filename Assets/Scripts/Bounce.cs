using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Movement Target = other.gameObject.GetComponent<Movement>();
        if (Target == null)
            return;
        //Target.AddForce(new BounceForce(other.gameObject.GetComponent<Rigidbody2D>().velocity, other, 5));
        Target.AddForce(new Force(new Vector2(0, 4)));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
     
        
    }

}
