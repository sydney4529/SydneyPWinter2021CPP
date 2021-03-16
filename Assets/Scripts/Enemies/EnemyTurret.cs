using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class EnemyTurret : MonoBehaviour
{
    public Transform projectileSpawnPointRight;
    public Transform projectileSpawnPointLeft;
    public Projectile projectile;

    public int distance;

    SpriteRenderer turretSprite;

    public GameObject playerInstance;

    public float projectileForce;
    public float projectileFireRate;

    float timeSinceLastFire = 0.0f;
    public int health;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        turretSprite = GetComponent<SpriteRenderer>();

        if(projectileForce <= 0)
        {
            projectileForce = 7.0f;
        }

        if(health <= 0)
        {
            health = 5;
        }

        if(distance <= 0)
        {
            distance = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(playerInstance.transform.position, turretSprite.gameObject.transform.position) < distance)
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetBool("Fire", true);
                timeSinceLastFire = Time.time;
            }
        }
        else
        {

        }

        if( turretSprite.flipX && playerInstance.transform.position.x > turretSprite.gameObject.transform.position.x || !turretSprite.flipX && playerInstance.transform.position.x < turretSprite.gameObject.transform.position.x)
        {
            turretSprite.flipX = !turretSprite.flipX;
        }
    }

    public void Fire()
    {
        //firing function
        if (turretSprite.flipX)
        {
            Projectile temp = Instantiate(projectile, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
            temp.speed = projectileForce * -1;
        }
        else
        {
            Projectile temp = Instantiate(projectile, projectileSpawnPointRight.position, projectileSpawnPointRight.rotation);
            temp.speed = projectileForce;
        }
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PlayerProjectile")
        {
            health--;
            Destroy(collision.gameObject);
            if(health <= 0)
            {
                Destroy(gameObject);
                GameManager.instance.score++;
            }
        }
    }
}
