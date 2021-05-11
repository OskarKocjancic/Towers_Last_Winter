using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    [SerializeField] private Animator m_Animator;
    [SerializeField] private float m_JumpForce = 1000f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private float m_GroundOffset;
    [SerializeField] private float m_HangTime = .2f;
    [SerializeField] private float m_MoveSpeed = 10f;
    [SerializeField] [Range(0.00f, 10.00f)] private float m_MaxFallingVelocity = 0.00f;

    private Vector3 emptyVelocity = Vector3.zero;
    [SerializeField] private float startPosY;
    [SerializeField] private float m_hangCounter;
    private float m_gravityScale;
    [SerializeField] private bool m_isGrounded;
    [SerializeField] private bool m_isFalling;
    private bool m_canRelease;
    private bool jumpInput;
    private bool jumpRInput;
    private bool _jumpInput;
    private bool _jumpRInput;
    private float _horizonatalAxisRaw;
    private Rigidbody2D rb;
    private BoxCollider2D bd;
    private SpriteRenderer sr;
    [SerializeField] private ParticleSystem dustParticles;
    [SerializeField] public Transform startingPosition;

    private void Awake  ()
    {
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        m_gravityScale = rb.gravityScale;
        
    }

    private void Update()
    {
        _horizonatalAxisRaw = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
            {
                jumpInput = true;
                jumpRInput = false;
            }

            if (Input.GetButtonUp("Jump"))
            {
                jumpRInput = true;
            }

        updateAnimation();
    }

    private void FixedUpdate()
    {
        //Debug.DrawRay(bd.bounds.center, new Vector2(0, m_GroundOffset * bd.bounds.extents.y*-1), Color.red);

        _jumpInput = jumpInput;
        _jumpRInput = jumpRInput;

        isGrounded(); 
        isFalling();
        //if player is falling disable all jump input and increase gravity
        if (m_isFalling)
        {
            rb.gravityScale = m_gravityScale * 2;
            jumpInput = jumpRInput = false;
        }
        else {
            rb.gravityScale = m_gravityScale;
        }
        //if player is grounded, remeber his y position for hangtime detection
        if (m_isGrounded)
        {
            startPosY = transform.position.y;
        }




        MoveLeftRight();

        //jump and hangtime control
        if (jumpInput && m_isGrounded)
            Jump();
        else
        {
            if (m_isGrounded)
            {
                m_hangCounter = m_HangTime;
            }
            else {
                m_hangCounter -= Time.deltaTime;
            }
            if (_jumpInput&&m_hangCounter>0f&&transform.position.y<startPosY)
            {
                Jump();
            }
        }
        if (jumpRInput&&m_canRelease)
        {
            JumpRelease();
        }
        
        
        
    }


    
    private void isGrounded()
    {
        
        //check if player is grounded
        RaycastHit2D raycastHit = Physics2D.BoxCast(bd.bounds.center, new Vector3(bd.bounds.size.x * 0.6f, bd.bounds.size.y, bd.bounds.size.z), 0f, Vector2.down, m_GroundOffset, m_WhatIsGround);
        m_isGrounded = raycastHit.collider != null;

    }


    private void isFalling()
    {
        //check if player is falling
        if (m_isGrounded)
        {
            m_isFalling = false;
        }
        else
        {
            float currentVelocity = rb.velocity.y;
            m_isFalling = currentVelocity < m_MaxFallingVelocity * (-1);
        }
    }

    private void MoveLeftRight() {
        //control movement left and right with control smoothing
        Vector3 targetVelocity = new Vector2(_horizonatalAxisRaw * m_MoveSpeed* Time.deltaTime * 100f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref emptyVelocity, .05f);
    }

    private void Jump() {
        //control jump
        changeDust(true);
        rb.velocity = new Vector2(rb.velocity.x*0.5f, 0f);
        rb.AddForce(new Vector2(0f, m_JumpForce));
        m_canRelease = true;
        jumpInput = false;
 
        
    }
    private void JumpRelease() {
        //check if jump button released 
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        m_canRelease = false;
        jumpRInput = false;

    }

    private void updateAnimation() {

        if (_horizonatalAxisRaw >0)
        {
            sr.flipX = false;
        }
        else if (_horizonatalAxisRaw < 0)
        {
            sr.flipX = true;
        }


        m_Animator.SetFloat("runSpeed", Mathf.Abs(_horizonatalAxisRaw));
        m_Animator.SetBool("isFalling", m_isFalling);

        if (rb.velocity.y > 1f)
        {
            m_Animator.SetBool("isJumping", true);
        }
        else {
            m_Animator.SetBool("isJumping", false);
        }
    }
    private void changeDust (bool enable){
        if (enable)
        {
            dustParticles.Play();
        }
        else
        {
            dustParticles.Stop();
        }
        
    }
}
