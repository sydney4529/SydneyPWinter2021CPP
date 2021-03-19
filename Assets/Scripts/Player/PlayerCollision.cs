using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]

public class PlayerCollision : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerMovement pm;

    public AudioSource dieSource;
    public BoxCollider2D playerBox;

    public AudioClip playerDie;

    public float bounceForce;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        dieSource = GetComponent<AudioSource>();
        playerBox = GetComponent<BoxCollider2D>();

        if (bounceForce <= 0)
        {
            bounceForce = 20.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dieSource)
        {
            dieSource = gameObject.AddComponent<AudioSource>();
            dieSource.clip = playerDie;
            dieSource.loop = false;
            //dieSource.Play();
        }
        else
        {
           // dieSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Squished")
        {
            if (!pm.isGrounded)
            {
                collision.gameObject.GetComponentInParent<EnemyWalker>().isSquished();
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * bounceForce);
                GameManager.instance.score++;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            playerBox.enabled = false;
            //dieSource.Play();
            GameManager.instance.lives--;
            Destroy(collision.gameObject);
            dieSource.Play();
            //Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Enemy")
        {
            playerBox.enabled = false;
            //dieSource.Play();
            GameManager.instance.lives--;
            dieSource.Play();
            //Destroy(gameObject);
        }

    }
}
