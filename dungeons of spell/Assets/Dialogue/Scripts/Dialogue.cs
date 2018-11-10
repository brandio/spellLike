using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(DialogueLoader))]
public class Dialogue : MonoBehaviour {

    [HideInInspector]
    public static Dialogue instance;

    public DialogueParticipant player;
    public DialogueUI dialogueUi;

    public DialogueLoader dialogueLoader;

    DialogueParticipant mLhsParticipant;
    DialogueParticipant mRhsParticipant;

    public Controls characterControls;

    public delegate void conversationEndedHandler(int endId);
    public event conversationEndedHandler conversationEnded;
    List<DialogueLoader.Conversation> currentConversations;
    //int i = 0;

    //void Update()
    //{
    //    if (i > 10 && i < 12)
    //    {
    //        int[] ids = { 0 };
    //        StartConversation(ids, null, null);
    //    }
    //    i++;
    //}

    public void InitConversation(int[] conversationId, conversationEndedHandler ended, DialogueParticipant lhsParticipant, DialogueParticipant rhsParticipant)
    {

        characterControls.active = false;
        if (ended != null)
        {
            conversationEnded += ended;
        }

        if(lhsParticipant == null)
        {
            lhsParticipant = player;
        }

        mLhsParticipant = lhsParticipant;
        mRhsParticipant = rhsParticipant;
        dialogueUi.lhsSprite.sprite = lhsParticipant.sprite;
        dialogueUi.rhsSprite.sprite = rhsParticipant.sprite;
        dialogueUi.lhsTitle.text = lhsParticipant.participantName;
        dialogueUi.rhsTitle.text = rhsParticipant.participantName;

        StartConversation(conversationId, lhsParticipant, rhsParticipant);
    }

    void StartConversation(int[] conversationId, DialogueParticipant lhsParticipant, DialogueParticipant rhsParticipant)
    {
        if(characterControls  != null)
        {
            characterControls.active = false;
        }
        currentConversations = dialogueLoader.GetConversations(conversationId);

        if (currentConversations[0].shake == 1)
        {
            ScreenShake.instance.shake(1);
        }
        List<string> strings = new List<string>();
        foreach (DialogueLoader.Conversation coversation in currentConversations)
        {
            strings.Add(coversation.text);
        }
        dialogueUi.DisplayText(currentConversations[0].side, strings);
    }

    public void UpdateConversation(int option)
    {
        //if(currentConversations[option].shake == 1)
        //{
        //    ScreenShake.instance.shake(1);
        //}
        if(currentConversations[option].childrenIds.Length == 0)
        {
            EndConversation(currentConversations[0].id);
        }
        else
        {
            StartConversation(currentConversations[option].childrenIds, mLhsParticipant, mRhsParticipant);
        }
    }

    void EndConversation(int endId)
    {
        if(conversationEnded != null)
        {
            foreach (conversationEndedHandler d in conversationEnded.GetInvocationList())
            {
                d(endId);
                conversationEnded -= d;
            }
        }
        dialogueUi.lhsPanel.SetActive(false);
        dialogueUi.rhsPanel.SetActive(false);

        if (characterControls != null)
        {
            characterControls.active = true;
        }
    }

    void Start()
    {
        if(instance != null)
        {
            Debug.LogError("SINGLETON");
        }
        else
        {
            instance = this;
        }
    }
}
