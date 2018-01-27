using UnityEngine;
using System.Collections;
/*
    This is a simple script for dropping coins. It subscribes to the EnemyDied event and drops 
    numberOfCoins when it is called.
 */
[RequireComponent (typeof (Health))]
public class CoinDrop : MonoBehaviour {
    public int numberOfCoins;

	void Start () {
        Health health = gameObject.GetComponent<Health>();
        health.EnemyDied += DropCoin;
	}

	void DropCoin (Health health) {
	    for(int i = 0; i < numberOfCoins; i++)
        {
            GameObject coin = OrbPool.instance.CheckOutOrb();
            coin.transform.position = new Vector2(transform.position.x + Random.Range(-1f, 1f), transform.position.y + Random.Range(-1f, 1f));
        }
	}
}
