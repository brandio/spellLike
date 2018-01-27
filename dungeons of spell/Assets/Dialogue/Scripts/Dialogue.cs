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

    DialogueLoader.Conversation currentConversation;
    int i;
    void Update()
    {
        if (i > 10 && i < 12 )
        {
            StartConversation(0, null, null);
        }
        i++;
    }
    void StartConversation(int conversationId, DialogueParticipant lhsParticipant, DialogueParticipant rhsParticipant)
    {
        mLhsParticipant = lhsParticipant;
        mRhsParticipant = rhsParticipant;
        if(characterControls  != null)
        {
            characterControls.enabled = false;
        }
        currentConversation = dialogueLoader.GetConversation(conversationId);
        List< string > strings = new List<string>() { currentConversation.text };
        dialogueUi.DisplayText(currentConversation.side, strings);
    }

    public void UpdateConversation()
    {
        
        if(currentConversation.childrenIds.Length == 0)
        {
            EndConversation();
        }
        else
        {
            StartConversation(currentConversation.childrenIds[0], null, null);
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
