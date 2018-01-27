using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelDisplay : MonoBehaviour {
    Text text;
    public CharacterLevel level;
    void Start () {
        text = GetComponent<Text>();
        level.LeveledUp += UpdateDisplay;
        level.GainedExperience += UpdateDisplay;
        UpdateDisplay(level);
    }
	
    void UpdateDisplay(CharacterLevel leveler)
    {
        text.text = ("Level " + leveler.GetLevel() + "\n" + leveler.GetExperience() + "/" + leveler.GetExperienceToLevel());
    }
}
