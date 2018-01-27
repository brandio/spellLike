using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class SpellCreationStone : MonoBehaviour
{
    public GameObject overWorld;
    CircleCollider2D trigger;
    bool playerInside = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerInside = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.Q))
        {
            overWorld.SetActive(false);
            SpellCreationInterface.instance.Open(true, 0);

        }
        else if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            overWorld.SetActive(false);
            SpellCreationInterface.instance.Open(false, 0);

        }
    }
}
