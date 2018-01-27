using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileParentPool : MonoBehaviour {

	List<GameObject> unactiveObjects = new List<GameObject>();
	public static ProjectileParentPool projectileParentPoolInstance;
    public GameObject objectToInstantiate;
	// Use this for initialization
	void Awake () {
		projectileParentPoolInstance = this;
		GameObject[] objects = GameObject.FindGameObjectsWithTag("ProjectileParent");
		foreach (GameObject obj in objects) {
			obj.SetActive(false);
			unactiveObjects.Add (obj);
		}
	}
	
	public static GameObject Take()
	{
        if (projectileParentPoolInstance.unactiveObjects.Count == 0)
        {
            GameObject newObject = GameObject.Instantiate(projectileParentPoolInstance.objectToInstantiate, Vector2.zero, Quaternion.identity) as GameObject;
            return newObject;
        }
        GameObject bulletObject = projectileParentPoolInstance.unactiveObjects[projectileParentPoolInstance.unactiveObjects.Count - 1];
		projectileParentPoolInstance.unactiveObjects.RemoveAt (projectileParentPoolInstance.unactiveObjects.Count - 1 );
		return bulletObject;
	}
	
	public static GameObject Take(Vector2 position, Quaternion rotation)
	{
		if (projectileParentPoolInstance.unactiveObjects.Count == 0) {
			Debug.LogError ("Object pool ran out");
			return null;
		}
		GameObject bulletObject = projectileParentPoolInstance.unactiveObjects[projectileParentPoolInstance.unactiveObjects.Count - 1];
		projectileParentPoolInstance.unactiveObjects.RemoveAt (projectileParentPoolInstance.unactiveObjects.Count - 1);
		bulletObject.transform.position = position;
		bulletObject.transform.rotation = rotation;
		return bulletObject;
	}
	
	public static void Give(GameObject obj)
	{
        if(obj != null)
        {
            obj.SetActive(false);
            projectileParentPoolInstance.unactiveObjects.Add(obj);
        }
	}
}
