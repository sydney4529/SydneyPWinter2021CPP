using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]

public class PlayerFire : MonoBehaviour
{
    SpriteRenderer marioSprite;
    AudioSource fireAudioSource;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;

    public float projectileSpeed;
    public Projectile projectilePrefab;

    public AudioClip fireSFX;

    // Start is called before the first frame update
    void Start()
    {
        marioSprite = GetComponent<SpriteRenderer>();
        fireAudioSource = GetComponent<AudioSource>();

        if (projectileSpeed <=0)
        {
            projectileSpeed = 7.0f;
        }

        if(!spawnPointLeft || !spawnPointRight || !projectilePrefab)
        {
            Debug.Log("Unity Inspector values not set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 1)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireProjectile(); 

                if (!fireAudioSource)
                {
                    fireAudioSource = gameObject.AddComponent<AudioSource>();
                    fireAudioSource.clip = fireSFX;
                    fireAudioSource.loop = false;
                    fireAudioSource.Play();
                }
                else
                {
                    fireAudioSource.Play();
                }
            }
        }
      
    }

    void FireProjectile()
    {
        if (marioSprite.flipX)
        {
            //Debug.Log("fire left");
            Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            projectileInstance.speed = projectileSpeed * -1;
            Physics2D.IgnoreCollision(projectileInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            //Debug.Log("fire right");
            Projectile projectileInstance = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            projectileInstance.speed = projectileSpeed;
            Physics2D.IgnoreCollision(projectileInstance.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
