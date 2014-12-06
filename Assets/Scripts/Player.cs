using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Inputs
    public float MovementInput = 0f;
    public bool JumpInput = false;
    //forces
    public float MovementForce = 5f;
    public float JumpForce = 50f;
    //Max values
    public float MaxSpeed = 5f;

    private bool IsGrounded = false;
    [HideInInspector]
    public bool FacingRight = true;

    private Transform GroundCheck;
    public float GroundCheckDist = 0.025f;

	// Use this for initialization
	void Start () 
    {
        GroundCheck = transform.FindChild("GroundCheck");
	}
	
    void Update()
    {
        MovementInput = Input.GetAxis("Horizontal");
        JumpInput = Input.GetButton("Jump");
    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        float jumpForce = 0f;
        float moveForce = 0f;
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckDist, 1 << LayerMask.NameToLayer("Ground"));
        if(IsGrounded && JumpInput)
        {
            jumpForce = JumpForce;
        }
        
        moveForce = MovementInput * MovementForce;

        rigidbody2D.AddForce(new Vector2(moveForce, jumpForce));

        if(Mathf.Abs(rigidbody2D.velocity.x) > MaxSpeed)
        {
            rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * MaxSpeed, rigidbody2D.velocity.y);
        }
        if (MovementInput > 0 && !FacingRight)
        {
            Flip();
        }
        else if (MovementInput < 0 && FacingRight)
        {
            Flip();
        }
	}

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        FacingRight = !FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
