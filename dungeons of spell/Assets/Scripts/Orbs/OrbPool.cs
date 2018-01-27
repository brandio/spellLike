using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class OrbPool : MonoBehaviour {
    List<GameObject> orbs;
    List<GameObject> coins;
    public static OrbPool instance;
    public GameObject orb;
	// Use this for initialization
	void Start () {
        if(instance != null)
        {
            Debug.LogError("Already have an instance lol");
        }
        else
        {
            instance = this;
        }

        orbs = new List<GameObject>();
        foreach(Transform child in transform)
            {
                orbs.Add(child.gameObject);   
            }
	}
	
	// Update is called once per frame
	public GameObject CheckOutOrb () {
        if (orbs.Count < 10)
        {
            GameObject orbObject = GameObject.Instantiate(instance.orb, Vector2.zero, Quaternion.identity) as GameObject;
            return orbObject;
        }
        GameObject orb = orbs[orbs.Count - 1];
        orbs.RemoveAt(orbs.Count - 1);
        orb.SetActive(true);
        return orb;
    }

    // Update is called once per frame
    public void CheckInOrb(GameObject orb)
    {
        orbs.Add(orb);
        orb.SetActive(false);
    }
}
