using UnityEngine;
using System.Collections;

public class HideOnKeyPress : MonoBehaviour {
    public GameObject objectToHide;
	bool requireControls = true;
	void HideIfKeyPress()
	{
		if(Input.GetKeyDown(KeyCode.M) )
		{
			if(!requireControls || Controls.GetInstance().active)
			{
				objectToHide.SetActive(!objectToHide.activeSelf);
			}
            
		}
	}
	// Update is called once per frame
	void Update () {
		HideIfKeyPress ();
	}
}
