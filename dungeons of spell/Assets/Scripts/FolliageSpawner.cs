using UnityEngine;
using System.Collections;

public class FolliageSpawner : MonoBehaviour {
    public Sprite[] sprites;
    public GameObject[] gameObjects;
    public float maxRotation;
    public float minRotation;
    public float minSize;
    public float maxSize;
    public float range;
    public int minElements;
    public int maxElements;

    void Start()
    {
        int numberOfElements = Random.Range(minElements, maxElements + 1);
        for(int i = 0; i < numberOfElements; i++)
        {
            GameObject go;
            if (sprites.GetLength(0) > 0)
            {
                go = new GameObject();
                go.AddComponent<SpriteRenderer>();
                go.AddComponent<YSort>();
                go.GetComponent<SpriteRenderer>().sortingLayerName = "BackGround";
                go.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.GetLength(0))];
            }
            else
            {
                go = GameObject.Instantiate(gameObjects[Random.Range(0, gameObjects.GetLength(0))],Vector2.zero,Quaternion.identity) as GameObject;
            }

            go.transform.parent = transform;
            go.transform.localPosition = Random.insideUnitCircle * range;
            if(Mathf.Abs(minRotation - maxRotation) > .001)
            {
                go.transform.eulerAngles = new Vector3(0, 0, Random.Range(minRotation, maxRotation));
            }
            if(Mathf.Abs(minSize - maxSize) > .001)
            {
                float scale = Random.Range(minSize, maxSize);
                go.transform.localScale = new Vector3(scale, scale, 1);
            }
            
            
            
        }
    }
}
