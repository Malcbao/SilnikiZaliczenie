using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Movement {

    bool bDirection = false;
    [SerializeField] Transform FirstPosition;
    [SerializeField] Transform SecPosition;
    protected override void Update()
    {
        Move(CalculateMoveDirection());
        base.Update();
    }

    Vector2 CalculateMoveDirection()
    {
        if (FirstPosition == null || SecPosition == null)
            return new Vector2(0, 0);
        if (bDirection && transform.position.x >= FirstPosition.position.x)
        {
            bDirection = false;
        }
        else if(!bDirection && transform.position.x <= SecPosition.position.x)
        {
            bDirection = true;
        }
        if (bDirection)
            return new Vector2(1, 0);
        return new Vector2(-1, 0);
    }
}
