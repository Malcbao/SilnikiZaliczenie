using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    [SerializeField] bool bIsUnlimited = false;
    [SerializeField] byte Health = 4;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool UsePoint(GameObject Actor)
    {
        if (Health > 0)
        {
            Actor.transform.position = gameObject.transform.position;
            Actor.GetComponent<Hero>().HealHero();
            if (!bIsUnlimited)
                Health--;
            return true;
        }

        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;
        collision.gameObject.GetComponent<Hero>().SetCP(this);
    }
}
