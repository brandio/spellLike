using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OrbDisplay : MonoBehaviour {

    Text text;
    public CoinHolder holder;
    void Start()
    {
        text = GetComponent<Text>();
        holder.GainedCoin += UpdateDisplay;
        UpdateDisplay(holder);
    }

    void UpdateDisplay(CoinHolder holder)
    {
        text.text = ("" + holder.GetCoinCount());
    }
}
