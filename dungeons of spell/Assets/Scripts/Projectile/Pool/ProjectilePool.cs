using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ProjectilePool : MonoBehaviour {

	List<GameObject> unactiveObjects = new List<GameObject>();
	public static ProjectilePool projectilePoolInstance;
    public GameObject objectToInstantiate;
    // Use this for initialization
    void Awake () {
		if (projectilePoolInstance != null) {
			return;
		}
		projectilePoolInstance = this;
		GameObject[] objects = GameObject.FindGameObjectsWithTag("Projectile");
		foreach (GameObject obj in objects) {
			obj.SetActive(false);
			unactiveObjects.Add (obj);
		}
	}

	public static GameObject Take()
	{
		if (projectilePoolInstance.unactiveObjects.Count == 0) {
            GameObject newObject = GameObject.Instantiate(projectilePoolInstance.objectToInstantiate, Vector2.zero, Quaternion.identity) as GameObject;
            return newObject;
        }
		GameObject bulletObject = projectilePoolInstance.unactiveObjects[projectilePoolInstance.unactiveObjects.Count - 1];
		projectilePoolInstance.unactiveObjects.RemoveAt (projectilePoolInstance.unactiveObjects.Count - 1 );
		return bulletObject;
	}

	public static GameObject Take(Vector2 position, Quaternion rotation)
	{
		if (projectilePoolInstance.unactiveObjects.Count == 0) {
			Debug.LogError ("Object pool ran out");
			return null;
		}
		GameObject bulletObject = projectilePoolInstance.unactiveObjects[projectilePoolInstance.unactiveObjects.Count - 1];
		projectilePoolInstance.unactiveObjects.RemoveAt (projectilePoolInstance.unactiveObjects.Count - 1);
		print (bulletObject.activeSelf);
		bulletObject.transform.position = position;
		bulletObject.transform.rotation = rotation;
		return bulletObject;
	}

	public static void Give(GameObject obj)
	{
		obj.SetActive(false);
		projectilePoolInstance.unactiveObjects.Add (obj);

	}
	
	// Update is called once per frame
	void Update () {
        if(unactiveObjects.Count < 1200)
        {
            for(int i = 0; i < 5; i++)
            {
                GameObject newObject = GameObject.Instantiate(projectilePoolInstance.objectToInstantiate, Vector2.zero, Quaternion.identity) as GameObject;
                newObject.SetActive(false);
                unactiveObjects.Add(newObject);
            }

        }
	}
}
