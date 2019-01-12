using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//W razie potrzebny należy poprawidz chodzenie po krzywych poiweszniach

public class Force
{
    
    protected Vector2 Direction;
    protected float DecresePower;
    public bool bPernament;
    public bool bCancelOnGround;
    public bool bDelayedCancel;

    public Force()
    {
        bPernament = false;
        bDelayedCancel = false;
    }

    public Force(Vector2 NewDirection)
    {
        Direction = NewDirection;
        DecresePower = 1;
        bPernament = false;
    }

    public Force(Vector2 NewDirection, float NewPower)
    {
        Direction = NewDirection;
        DecresePower = NewPower;
        bPernament = false;
    }

    void DecreseForce()
    {
        if (DecresePower == 0)
            return;

        float dirX = Mathf.MoveTowards(Direction.x, 0, 1 * DecresePower * Time.deltaTime);
        float dirY = Mathf.MoveTowards(Direction.y, 0, 1 * DecresePower * Time.deltaTime);
        Direction = new Vector2(dirX, dirY);
    }

    public Vector2 GetForce()
    {
        Vector2 Dar = Direction;
        DecreseForce();
        return Dar;
    }

    public IEnumerator CancelOnGroundDelay()
    {
        yield return new WaitForSeconds(0.1f);
        bCancelOnGround = true;
    }
}

public class BounceForce : Force
{
    public BounceForce(Vector2 NewDirection, float NewPower) : base(NewDirection, NewPower)
    {
        bCancelOnGround = true;
    }

    public BounceForce(Vector2 MoveVelocity, Vector2 ColisionPoint, float NewPower)
    {
        ColisionPoint = ColisionPoint.normalized;
        bDelayedCancel = true;
        bCancelOnGround = false;
        if(ColisionPoint.x > ColisionPoint.y)
        {

        }
        
        DecresePower = NewPower;
    }
}

public class PernamentForce : Force
{
    public PernamentForce(Vector2 NewDirection, float NewPower) : base(NewDirection, NewPower)
    {
        bPernament = true;
    }
}


public class Movement : MonoBehaviour {

    protected float MovementControl;
    [SerializeField] float WalkSpeed = 250;
    [SerializeField] float Gravity = 1;
    [SerializeField] float JumpPower = 3;
    protected Rigidbody2D rb;
    protected bool bIsOnGround;

    Rigidbody2D Floor;

    [SerializeField] Vector2 Size;
    
    Vector2 MoveForce;
    float FallSpeed;
    bool bIsJumping;
    List<Force> Forces;

    // Use this for initialization
    protected virtual void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        Forces = new List<Force>();
    }
	
	// Update is called once per frame
	protected virtual void Update () {
        IsOnGround();
        rb.velocity = GetMovementDirection();
    }


    protected void Move(Vector2 Direction)
    {
        if (bIsOnGround)
            MoveForce = Direction;
        else
        {
            MoveForce = new Vector2(Mathf.Clamp(MoveForce.x + Direction.x * Time.deltaTime * 1, -2.5f, 2.5f), 0);
            if(Direction.x > -0.1f && Direction.x < 0.1f)
            {
                MoveForce = Vector2.MoveTowards(MoveForce, new Vector2(0, 0), 1 * Time.deltaTime);
            }
        }
    }


    Vector2 GetMovementDirection()
    {
        Vector2 moveDar = MoveForce * WalkSpeed * Time.deltaTime;
        if(bIsOnGround)
        {
            if (!bIsJumping)
            {
                FallSpeed = 0;
            }
            else
            {
                bIsJumping = false;
            }
        }
        else
        {
            FallSpeed = Mathf.Clamp(FallSpeed - 3 * Gravity * Time.deltaTime, -5, 5);
        }
        moveDar = new Vector2(moveDar.x, moveDar.y + FallSpeed);
        
        Vector2 ActualForcePower;
        /*foreach(Force force in Forces)
        {
            ActualForcePower = force.GetForce();
            if (ActualForcePower.magnitude == 0)
                Forces.Remove(force);
            else
            {
                moveDar += ActualForcePower;
            }
        }*/
        //nie użyłem foreach ze względu na usuwanie elementu listy podczas pętli
        for(int i = 0; i < Forces.Count; i++)
        {
            
            if(Forces[i].bCancelOnGround && bIsOnGround)
            {
                Forces.Remove(Forces[i]);
                i--;
                continue;
            } else if (Forces[i].bDelayedCancel)
            {
                StartCoroutine(Forces[i].CancelOnGroundDelay());
                Forces[i].bDelayedCancel = false;
            }
            ActualForcePower = Forces[i].GetForce();
            if (ActualForcePower.magnitude > 0)
            {
                moveDar += ActualForcePower;
            }
            else if(!Forces[i].bPernament)
            {
               Forces.Remove(Forces[i]);
               i--;
            }
        }
        //Debug.Log(Forces.Count);

        if(Floor)
        {
            moveDar += Floor.velocity;
        }
        return moveDar;
    }

    protected void Jump()
    {
        if(bIsOnGround)
        {
            FallSpeed = JumpPower;
            bIsJumping = true;
        }
    }


    void IsOnGround()
    {   
        Floor = FindFloor();   
        /*if (GroundCheck == null)
        {
            bIsOnGround = false;
            return;
        }
        LayerMask Mask = LayerMask.GetMask("Gameplay");
        Collider2D Ground = Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - Height / 2), new Vector2(0.5f, 0.001f), 0.0f, Mask);   
        if (!Ground)
        {
            bIsOnGround = false;
            return;
        }
        bIsOnGround = Ground.gameObject;*/
    }

    Rigidbody2D FindFloor()
    {
        bIsOnGround = false;
        CapsuleDirection2D Dir = CapsuleDirection2D.Vertical;
        if (Size.x > Size.y)
        {
            Dir = CapsuleDirection2D.Horizontal;
        }
        LayerMask Mask = LayerMask.GetMask("Gameplay");
        //bIsOnGround = Physics2D.OverlapCapsule(transform.position, Size, Dir, 0, Mask);
        //Collider2D Ground = Physics2D.OverlapCapsule(transform.position, Size, Dir, 0, Mask);
        Collider2D[] Ground = Physics2D.OverlapCapsuleAll(transform.position, Size, Dir, 0, Mask);
        foreach (Collider2D col in Ground)
        {
            if (col.gameObject != gameObject)
            {
                bIsOnGround = true;
                if(col.GetComponent<Rigidbody2D>())
                return col.GetComponent<Rigidbody2D>();
            }
        }
        return null;
    }


    public void AddForce(Force NewForce)
    {
        Forces.Add(NewForce);
    }

    public void RemoveForce(Force OldForce)
    {
        Forces.Remove(OldForce);
    }


}
