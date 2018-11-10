using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(DialogueParticipant))]
public class DialogueMan : InRangeActive {
    public int[] ids;
    public bool force = false;
    bool forced = false;
    [System.Serializable]
    public struct IdEvents
    {
        public int id;
        public IScriptedEvent scriptedEvent;
    }
    public IdEvents[] events;

    public Dictionary<int, IScriptedEvent> eventsDic = new Dictionary<int, IScriptedEvent>();
    void conversationOverHandlerer(int id)
    {
        if(eventsDic.ContainsKey(id))
        {
            eventsDic[id].StartEvent();
        }
    }
    // Use this for initialization
    void Start () {
	    foreach(IdEvents scriptEvent in events)
        {
            eventsDic.Add(scriptEvent.id, scriptEvent.scriptedEvent);
        }
	}
	
	// Update is called once per frame
	void Update () {
	    if((playerInside && Controls.GetInstance().active) && ((force && !forced) || (Input.GetKeyDown(KeyCode.E))))
        {
            forced = !forced;
            Dialogue.instance.InitConversation(ids, conversationOverHandlerer, null, this.GetComponent<DialogueParticipant>());
        }
	}
}
