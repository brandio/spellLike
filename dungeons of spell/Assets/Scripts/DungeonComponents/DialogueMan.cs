using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DialogueParticipant))]
public class DialogueMan : InRangeActive {
    public int[] ids;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(playerInside && Input.GetKeyDown(KeyCode.E))
        {
            Dialogue.instance.InitConversation(ids, null, null, this.GetComponent<DialogueParticipant>());
        }
	}
}
