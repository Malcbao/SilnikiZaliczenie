using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] List<GameObject> Healths;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void RemoveHealth(GameObject Health)
    {
        Healths.Remove(Health);
        if (Healths.Count == 0)
        {
            GameObject Manager = GameObject.FindGameObjectWithTag("MapManager");
            if (Manager)
            {
                Manager.GetComponent<MapManager>().EnemyKilled(gameObject);
            }
            Destroy(gameObject);
        }
    }
}
