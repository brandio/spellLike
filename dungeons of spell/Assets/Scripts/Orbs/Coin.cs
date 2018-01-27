using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {
    AudioSource source;
    SpriteRenderer render;
    bool ok = true;
    void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
        render = gameObject.GetComponent<SpriteRenderer>();
    }
    
    void OnEnable()
    {
        render.enabled = true;
        ok = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && ok)
        {
            ok = !ok;
            other.gameObject.GetComponent<CoinHolder>().AddCoin();
            render.enabled = false;
            source.Play();
            StartCoroutine("CoinRoutine");
        }
    }

    IEnumerator CoinRoutine()
    {
        yield return new WaitForSeconds(1);
        OrbPool.instance.CheckInOrb(gameObject);
    }
}
