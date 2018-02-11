using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Health))]
public class DamageRecorder : MonoBehaviour {
    public Text text;
    public float countTime = 1;
    float lastHealthAmount;
    Health health;
    public float damage = 0;
    bool tookDamage = false;
    float lastDamageTime = 0;

	// Use this for initialization
	void Start () {
        health = this.GetComponent<Health>();
        health.HealthChanged += TookDamage;
        lastHealthAmount = health.health;
        text.text = "";
    }

    void TookDamage(Health health)
    {
        Debug.Log(lastHealthAmount + " " + health.health);
        damage += lastHealthAmount - health.health;
        lastHealthAmount = health.health;
        tookDamage = true;
        lastDamageTime = Time.time;

    }
	// Update is called once per frame
	void Update () {
	    if(tookDamage && lastDamageTime + countTime < Time.time)
        {
            text.text = damage.ToString();
            tookDamage = false;
            damage = 0;
        }
        else if(tookDamage == false && lastDamageTime + countTime*2 < Time.time)
        {
            text.text = "";
        }
	}
}
