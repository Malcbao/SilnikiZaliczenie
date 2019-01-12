using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {

    bool bIsDamaged;
    CheckPoint ActualCP;

    // Use this for initialization
    void Start () {
        bIsDamaged = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void DealDamage()
    {
        if (bIsDamaged)
            KillHero();
        else
        {
            bIsDamaged = true;
            StopCoroutine(AfterDamageDelay());
            StartCoroutine(AfterDamageDelay());   
        }
    }

    public void KillHero()
    {
        if(ActualCP)
        {
            if(!ActualCP.UsePoint(gameObject))
            {

            }
        }
    }

    public void HealHero()
    {
        bIsDamaged = false;
    }

    IEnumerator AfterDamageDelay()
    {
        yield return new WaitForSeconds(5);
        HealHero();
    }


    public void SetCP(CheckPoint NewCP)
    {
        ActualCP = NewCP;
    }

}
