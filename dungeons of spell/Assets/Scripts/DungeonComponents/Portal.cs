using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CircleCollider2D))]
public class Portal : InRangeActive
{

    AudioSource source;
    public Room homeRoom;
    public Portal destinationPortal;

	// Use this for initialization
	void Start () {
        source = this.GetComponent<AudioSource>();
	}


    void Update()
    {

        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            CoinHolder holder = player.gameObject.GetComponent<CoinHolder>();
            if(source != null)
            {
                source.Play();
            }
            player.transform.position = destinationPortal.transform.position;
            PlayerRoomManager.instance.ChangeRoom(destinationPortal.homeRoom);
        }
    }

}
