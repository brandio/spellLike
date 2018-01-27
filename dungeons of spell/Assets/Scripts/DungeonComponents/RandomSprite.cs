using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SpriteRenderer))]
public class RandomSprite : MonoBehaviour {
    public Sprite[] sprites;

	void Start () {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
	}

}
