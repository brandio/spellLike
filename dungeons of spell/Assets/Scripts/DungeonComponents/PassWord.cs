using UnityEngine;
using System.Collections;
using System;
[RequireComponent (typeof (CircleCollider2D))]
public class PassWord : InRangeActive {

	public Component passwordProtectedComponent;
	IPasswordActive passWordActiveObj;
    public string passWord = "branden";

	public void SetPassWord(string pass)
	{
		passWord = pass;
	}

	// Use this for initialization
	void Start () {
		if (passwordProtectedComponent is IPasswordActive) {
			passWordActiveObj = (IPasswordActive)passwordProtectedComponent;
		} else {
			Debug.LogError("Component should implement IPasswordActive");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(playerInside && Input.GetKeyUp(KeyCode.E) && !InputText.IsActive())
		{
            InputText.OpenInputText(CheckPassWord);
		}
	}

    void CheckPassWord(string s)
    {
		if(s.Equals(passWord, StringComparison.InvariantCultureIgnoreCase))
        {
            passWordActiveObj.ActivatePassword();
        }
    }
}
