using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class Flashing : MonoBehaviour {
    public AnimationCurve curve;
    public float speed;
    Color defaultColor;
    float counter = 0;
    Text text;
    
	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
        defaultColor = text.color;
	}
	
	// Update is called once per frame
	void Update () {
        counter += Time.deltaTime * speed;
        if(counter > 1)
        {
            counter = 0;
        }
        text.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, defaultColor.a * curve.Evaluate(counter));
	}
}
