using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    [SerializeField] GameObject MyEnemy;
    [SerializeField] Transform UpLocation;

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;
        Debug.Log("HealthRemoved");
        Movement Target = collision.gameObject.GetComponent<Movement>();
        if (Target)
        {
            Target.AddForce(new BounceForce(GetUpVector()*5, 1));

        }
        if (MyEnemy)
        {
            MyEnemy.GetComponent<Enemy>().RemoveHealth(gameObject);
        }
        Destroy(gameObject);
    }

    public Vector2 GetUpVector()
    {
        return (UpLocation.position - transform.position).normalized;
    }


}
