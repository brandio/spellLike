using UnityEngine;
using System.Collections;

public class HideOnKeyPress : MonoBehaviour {
    public GameObject objectToHide;
    public Camera objectToShow;
	bool requireControls = true;
    
	void HideIfKeyPress()
	{
		if(Input.GetKeyDown(KeyCode.M) )
		{
			if(!requireControls || Controls.GetInstance().active)
			{
				objectToHide.SetActive(!objectToHide.activeSelf);
                //objectToShow.enabled = objectToShow.enabled;
            }
            
		}
	}
	// Update is called once per frame
	void Update () {
		HideIfKeyPress ();
	}
}
