using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class AttackOnDeath : MonoBehaviour {
    public EnemyShoot attack;
	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Health>().EnemyDied += DiedHandler;
    }
	
	// Update is called once per frame
	void DiedHandler (Health health) {
        attack.shoot();
    }
}
