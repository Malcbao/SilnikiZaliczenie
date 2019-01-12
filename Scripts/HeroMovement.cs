using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeroMovement : Movement
{

    // Use this for initialization
    protected override void Start () {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        if (Input.GetKeyDown("space"))
            Jump();
        if (Input.GetKeyDown("g"))
            AddForce(new Force(new Vector2(10, 5)));
    }

    private void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Move(new Vector2(moveHorizontal, 0));      
    }

    
}
