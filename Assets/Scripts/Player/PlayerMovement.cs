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
    AudioSource jumpAudioSource;

    public float speed;
    public int jumpForce;
    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    public bool isFiring = false;
    public bool isCape = false;

    public AudioClip jumpSFX;
    

    private Vector3 initialScale;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        marioSprite = GetComponent<SpriteRenderer>();
        jumpAudioSource = GetComponent<AudioSource>();
       

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

        if (Time.timeScale == 1)
        {

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                //make jump velocity always the same, comment out if you dont want it
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
                if (!jumpAudioSource)
                {
                    jumpAudioSource = gameObject.AddComponent<AudioSource>();
                    jumpAudioSource.clip = jumpSFX;
                    jumpAudioSource.loop = false;
                    jumpAudioSource.Play();
                }
                else
                {
                    jumpAudioSource.Play();
                }

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

            rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);
            anim.SetFloat("speed", Mathf.Abs(horizontalInput));

            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isFiring", isFiring);
            anim.SetBool("isCape", isCape);

            if (marioSprite.flipX && horizontalInput > 0 || !marioSprite.flipX && horizontalInput < 0)
            {
                marioSprite.flipX = !marioSprite.flipX;
            }
        }

    }

    public void startJumpForceChange()
    {
        StartCoroutine(JumpForceChange());
    }

    IEnumerator JumpForceChange()
    {
        jumpForce = 500;
        yield return new WaitForSeconds(2.0f);
        jumpForce = 350;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Pickups")
        {
            //Debug.Log("hit key");
            if (Input.GetKey(KeyCode.E))
            {
                Pickups curPickup = collision.GetComponent<Pickups>();
                switch (curPickup.currentCollectible)
                {
                    case Pickups.CollectibleType.KEY:
                        //add to inventory or other mechanic
                        //Destroy(collision.gameObject);
                        curPickup.transform.position = new Vector3(curPickup.transform.position.x, curPickup.transform.position.y, -100);
                        curPickup.pickupAudioSource.Play();
                        GameManager.instance.score++;
                        break;
                }
            }

        }
           
    }
}
