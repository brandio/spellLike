using UnityEngine;
using System.Collections;

public class RandomScale : MonoBehaviour {

    public float yMin = .1f;
    public float yMax = 1;
    public float xMin = .1f;
    public float xMax = 1;
    public bool square = false;
    
	// Update is called once per frame
	void Awake () {
        float sizeY = 0;
        float sizeX = Random.Range(xMin, xMax);
        if (square)
        {
            sizeY = sizeX;
        }
        else
        {
            sizeY = Random.Range(yMin, yMax);
        }
            
        transform.localScale = new Vector3(sizeX, sizeY, 1);
	}
}
