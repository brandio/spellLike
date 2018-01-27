using UnityEngine;
using System.Collections;
[RequireComponent (typeof (CircleCollider2D))]
public class MagnetTowardsPlayer : MonoBehaviour {

    public float speed;
    float tempSpeed;
    public bool playerInside = false;
    Transform player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.transform;
            playerInside = true;
        }
    }

    void OnEnable()
    {
        tempSpeed = speed;
        playerInside = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //playerInside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInside)
        {
            transform.parent.position = Vector2.MoveTowards(new Vector2(transform.parent.position.x, transform.parent.position.y), player.position, tempSpeed * Time.deltaTime);
            tempSpeed += 10f * Time.deltaTime;
        }
        
    }
}
