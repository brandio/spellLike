using UnityEngine;
using System.Collections;

public class EnemyAim : MonoBehaviour {

	Camera cam;
	Transform player;

    Movement playerMovment;
    bool aim = false;
    public int aimChance;
    float attackSpeed = 1.8f; // todo just guessing at this cus why not
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").transform;
        aim = Random.Range(0, 100) < aimChance;
        if(aim)
        {
            playerMovment = player.GetComponent<Movement>();
        }
        GameObject camera = GameObject.Find ("Main Camera");
		cam = camera.GetComponent<Camera> ();
	}
	
	void Aim()
	{
        Vector3 playerPos = player.position;
        if (aim)
        {
            float distance = Vector2.Distance(transform.position, player.position)/attackSpeed;
            playerPos = new Vector2(playerPos.x + playerMovment.direction.x * distance * playerMovment.speedMod, playerPos.y + playerMovment.direction.y * distance * playerMovment.speedMod);
        }
		Vector2 direction = playerPos - this.transform.position;
		
		float angle = Mathf.Atan (direction.y / direction.x) * Mathf.Rad2Deg;
		if (direction.x < 0) {
			angle = angle + 180;
		}
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}
	
	// Update is called once per frame
	void Update () {
		Aim ();
	}
}
