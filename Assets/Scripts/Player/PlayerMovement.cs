using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer marioSprite;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool isFiring = false;
    public bool isCape = false;

    private Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();

        initialScale = transform.localScale;

        if (speed <= 0)
        {
            speed = 5.0f;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 100;
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.01f;
        }

        if (!groundCheck)
        {
            Debug.Log("groundCheck does not exist, please set a transform value for groundCheck");
        }

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);



        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Debug.Log("Jumped");
            //make jump velocity always the same, comment out if you dont want it
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce);
            
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.UpArrow))
        {
            isCape = true;
        }
        else
        {
            isCape = false;
        }

        if (Input.GetKeyUp(KeyCode.Space) && Input.GetKeyUp(KeyCode.UpArrow))
        {
            isCape = false;
        }
      

        if (Input.GetButtonDown("Fire1"))
        {
            isFiring = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            isFiring = false;
        }

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
        //}

        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        //}

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
        anim.SetFloat("speed", Mathf.Abs(horizontalInput));

        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFiring", isFiring);
        anim.SetBool("isCape", isCape);

        if(marioSprite.flipX && horizontalInput > 0 || !marioSprite.flipX && horizontalInput < 0)
        {
            marioSprite.flipX = !marioSprite.flipX;
        }

    }
}
