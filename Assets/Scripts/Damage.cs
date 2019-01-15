using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour {

    [SerializeField] bool bIsCritical;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hero hero = collision.gameObject.GetComponent<Hero>();
        if (!hero)
            return;

        if (bIsCritical)
            hero.KillHero();
        else
            hero.DealDamage();
    }

}
