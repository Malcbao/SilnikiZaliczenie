using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burning : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        Hero Target = collision.gameObject.GetComponent<Hero>();
        if (!Target)
            return;

        Target.DealDamage();

        Movement TargetMovement = collision.gameObject.GetComponent<Movement>();
        if (!TargetMovement)
            return;
        Collider2D[] Coliders = new Collider2D[1];
        collision.GetContacts(Coliders);

        TargetMovement.AddForce(new BounceForce(collision.gameObject.GetComponent<Rigidbody2D>().velocity*2, Coliders[0].transform.position, 5));
    }
}
