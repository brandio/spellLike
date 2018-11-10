using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class DialogueUI : MonoBehaviour {

    public AudioSource talkingSound;
    public AudioSource optionSound;

    public GameObject rhsPanel;
    public GameObject lhsPanel;

    public Image rhsSprite;
    public Image lhsSprite;
    public Text rhsTitle;
    public Text lhsTitle;
    public Text rhsTextField;
    public float charactersPerSecond = 4;

    public enum DialogueUiState {rhs,lhs,lhsChoice,fillingLhs,fillingRhs }
    public DialogueUiState currentState = DialogueUiState.rhs;

    public Dialogue dialogue;

    public GameObject mDialogueObject;

    public List<Text> lhsTextFields;
    public List<Image> options;

    int selectedOption = 0;
    List<GameObject> dialogueObjects = new List<GameObject>();
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.E))
        {
            switch(currentState)
            {
                case DialogueUiState.fillingRhs:
                    currentState = DialogueUiState.rhs;
                    break;
                case DialogueUiState.fillingLhs:
                    currentState = DialogueUiState.lhs;
                    break;
                case DialogueUiState.rhs:
                case DialogueUiState.lhs:
                    dialogue.UpdateConversation(0);
                    break;
                case DialogueUiState.lhsChoice:
                    dialogue.UpdateConversation(selectedOption);
                    break;
            }
        }
        if (currentState == DialogueUiState.lhsChoice && (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)))
        {
            if((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) && selectedOption <= (options.Count - 1))
            {
                optionSound.Play();
                SetOption(selectedOption + 1);
            }
            else if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && selectedOption > 0)
            {
                optionSound.Play();
                SetOption(selectedOption - 1);
            }
        }
    }

    void SetOption(int option)
    {
        selectedOption = option;
        for(int i = 0; i < options.Count; i++)
        {
            bool enabled = option == i ? true : false;
            options[i].enabled = enabled;
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
            StartCoroutine(DisplayTextOverTime(intRhs, text));
        }
        else
        {
            if(textList.Count == 1)
            {
                lhsTextFields[0].enabled = false;
                lhsTextFields[1].enabled = false;
                lhsTextFields[2].enabled = true;
                SetOption(-1);
                StartCoroutine(DisplayTextOverTime(intRhs, textList[0]));
            }
            else
            {
                lhsTextFields[0].enabled = true;
                lhsTextFields[0].text = textList[0];
                lhsTextFields[1].enabled = true;
                lhsTextFields[1].text = textList[1];
                lhsTextFields[2].enabled = false;
                SetOption(0);
                currentState = DialogueUiState.lhsChoice;
            }
        }
    }

    IEnumerator DisplayTextOverTime(int rhs, string text)
    {
        float numberOfCharacters = 0;
        Text textField;
        if (rhs == 1)
        {
            talkingSound.panStereo = .9f;
            currentState = DialogueUiState.fillingRhs;
            textField = rhsTextField;
        }
        else
        {
            talkingSound.panStereo = .1f;
            currentState = DialogueUiState.fillingLhs;
            textField = lhsTextFields[2];
        }
        talkingSound.Play();
        while (currentState == DialogueUiState.fillingRhs || currentState == DialogueUiState.fillingLhs)
        {
            numberOfCharacters += charactersPerSecond * Time.deltaTime;

            if (numberOfCharacters >= text.Length)
            {
                numberOfCharacters = text.Length;
                if (currentState == DialogueUiState.fillingRhs)
                    currentState = DialogueUiState.rhs;
                if (currentState == DialogueUiState.fillingLhs)
                    currentState = DialogueUiState.lhs;
            }
            string textToInsert = text.Substring(0, (int)Mathf.Ceil(numberOfCharacters));
            textField.text = textToInsert;
            yield return null;
        }
        currentState = DialogueUiState.rhs;
        textField.text = text;
    }
}
