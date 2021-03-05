using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum CollectibleType
    {
        POWERUP,
        COLLECTIBLE,
        LIVES,
        KEY
    }

    public CollectibleType currentCollectible;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            switch (currentCollectible)
            {

                case CollectibleType.POWERUP:
                    Debug.Log("powerup");
                    collision.GetComponent<PlayerMovement>().startJumpForceChange();
                    Destroy(gameObject);
                    break;

                case CollectibleType.LIVES:
                    Debug.Log("lives");
                    GameManager.instance.lives++;
                    Destroy(gameObject);
                    break;

                case CollectibleType.COLLECTIBLE:
                    Debug.Log("colectible");
                    GameManager.instance.score++;
                    Destroy(gameObject);
                    break;

            }
        }

    }

}
