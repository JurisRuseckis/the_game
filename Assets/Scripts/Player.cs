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

    public bool IsGrounded = false;
    [HideInInspector]
    public bool FacingRight = true;

    private Transform GroundCheck;
	private Animator Anim;
	// Use this for initialization
	void Start () 
    {
        GroundCheck = transform.FindChild("GroundCheck");
		Anim = GetComponent<Animator> ();
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
        IsGrounded = Physics2D.OverlapCircle(GroundCheck.position, 0.025f, 1 << LayerMask.NameToLayer("Ground"));
		Anim.SetBool ("IsGrounded", IsGrounded);
        if(IsGrounded && JumpInput)
        {
            jumpForce = JumpForce;
        }
        
        moveForce = MovementInput * MovementForce;

        rigidbody2D.AddForce(new Vector2(moveForce, jumpForce));
		float Speed = Mathf.Abs (rigidbody2D.velocity.x);
		Anim.SetFloat ("Speed",Speed);

		if(Speed > MaxSpeed)
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
