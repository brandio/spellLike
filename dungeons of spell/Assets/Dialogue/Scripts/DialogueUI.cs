using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class DialogueUI : MonoBehaviour {

    public GameObject rhsPanel;
    public GameObject lhsPanel;

    public Text rhsTextField;
    public float charactersPerSecond = 4;

    public enum DialogueUiState {rhs,lhs,fillingRhs }
    public DialogueUiState currentState = DialogueUiState.rhs;

    public Dialogue dialogue;

    public GameObject mDialogueObject;

    public float dialoguePosition = 5;
    List<GameObject> dialogueObjects = new List<GameObject>();
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Return))
        {
            switch(currentState)
            {
                case DialogueUiState.fillingRhs:
                    currentState = DialogueUiState.rhs;
                    break;
                case DialogueUiState.rhs:
                    dialogue.UpdateConversation();
                    break;
                case DialogueUiState.lhs:
                    dialogue.UpdateConversation();
                    break;
            }
        }
    }

    public void DisplayText(int intRhs, List<string> textList)
    {
        bool rhs = intRhs == 1 ? true : false;
        lhsPanel.SetActive(!rhs);
        rhsPanel.SetActive(rhs);

        Debug.Log(textList[0]);
        if (rhs)
        {
            string text = textList[0];
            StartCoroutine(DisplayTextOverTime(text));
        }
        else
        {
            for(int i = 0; i < textList.Count; i++)
            {
                GameObject dialogueObject = GameObject.Instantiate(mDialogueObject, Vector2.zero, Quaternion.identity) as GameObject;
                dialogueObject.transform.SetParent(lhsPanel.transform);
                dialogueObject.transform.localPosition = new Vector2(0,i * dialoguePosition);
                dialogueObject.GetComponent<Text>().text = textList[i];
            }
        }
    }

    public IEnumerator DisplayTextOverTime(string text)
    {
        float numberOfCharacters = 0;
        currentState = DialogueUiState.fillingRhs;
        while (currentState == DialogueUiState.fillingRhs)
        {
            numberOfCharacters += charactersPerSecond * Time.deltaTime;
            
            if(numberOfCharacters >= text.Length)
            {
                numberOfCharacters = text.Length;
                if(currentState == DialogueUiState.fillingRhs)
                    currentState = DialogueUiState.rhs;
            }
            string textToInsert = text.Substring(0, (int)Mathf.Ceil(numberOfCharacters));
            rhsTextField.text = textToInsert;
            yield return null;
        }
        currentState = DialogueUiState.rhs;
        rhsTextField.text = text;
    }

}
