using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pull : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Vector2 MyForce = gameObject.transform.position - collision.gameObject.transform.position;
            MyForce.Normalize();
            if (MyForce.y > 0)
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(MyForce * 15);
            else
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

}
