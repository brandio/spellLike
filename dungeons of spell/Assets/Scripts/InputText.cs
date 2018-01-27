using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputText : MonoBehaviour {
    static InputText instance;

    public delegate void InputTextReturn(string text);
    InputTextReturn inputTextCallBack;
    bool active;
    public InputField inputText;

    public static void OpenInputText(InputTextReturn callBack)
    {
		instance.gameObject.SetActive(true);
        Controls.GetInstance().active = false;
        instance.active = true;
        instance.inputTextCallBack = callBack;
    }

	public static bool IsActive()
	{
		return instance.active;
	}
    void Start()
    {
        if(instance != null)
        {
            Debug.LogError("INSTANCE IS BAD");
        }
        instance = this;
        gameObject.SetActive(false);
    }
    
    void CloseInputText()
    {
		active = false;
		Controls.GetInstance().active = true;
		inputTextCallBack(inputText.text);
		inputText.text = "";
		gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
		if (active && (Input.GetKeyDown("enter") || Input.GetKeyDown("return")))
        {
            CloseInputText();
        }
	}
}
