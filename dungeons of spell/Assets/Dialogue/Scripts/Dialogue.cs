using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(DialogueLoader))]
public class Dialogue : MonoBehaviour {

    public DialogueUI dialogueUi;

    public DialogueLoader dialogueLoader;

    DialogueParticipant mLhsParticipant;
    DialogueParticipant mRhsParticipant;

    Controls characterControls;

    List<DialogueLoader.Conversation> currentConversations;
    int i;
    void Update()
    {
        if (i > 10 && i < 12 )
        {
            int[] ids = { 0 };
            StartConversation(ids, null, null);
        }
        i++;
    }
    void StartConversation(int[] conversationId, DialogueParticipant lhsParticipant, DialogueParticipant rhsParticipant)
    {
        mLhsParticipant = lhsParticipant;
        mRhsParticipant = rhsParticipant;
        if(characterControls  != null)
        {
            characterControls.enabled = false;
        }
        currentConversations = dialogueLoader.GetConversations(conversationId);
        List< string > strings = new List<string>() { currentConversations[0].text };
        dialogueUi.DisplayText(currentConversations[0].side, strings);
    }

    public void UpdateConversation()
    {
        
        if(currentConversations[0].childrenIds.Length == 0)
        {
            EndConversation();
        }
        else
        {
            StartConversation(currentConversations[0].childrenIds, null, null);
        }
    }

    void EndConversation()
    {
        if (characterControls != null)
        {
            characterControls.enabled = false;
        }
    }
}
