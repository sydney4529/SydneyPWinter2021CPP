using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class Projectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;

    // Start is called before the first frame update
    void Start()
    {
        if(lifeTime <= 0)
        {
            lifeTime = 2.0f;
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
        Destroy(gameObject, lifeTime);

        //Physics.IgnoreLayerCollision(0, 6);

    }

    void OnEnable()
    {
        GameObject[] powerUpObjects = GameObject.FindGameObjectsWithTag("PowerUp");

        foreach (GameObject PowerUp in powerUpObjects)
        {
            Physics2D.IgnoreCollision(PowerUp.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        GameObject[] collectibleObjects = GameObject.FindGameObjectsWithTag("Collectible");

        foreach (GameObject Collectible in collectibleObjects)
        {
            Physics2D.IgnoreCollision(Collectible.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "PowerUp" || collision.gameObject.tag == "Collectible")
        {
            //Debug.Log(collision.gameObject);
            //Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            //Debug.Log("i've hit a tag");
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
