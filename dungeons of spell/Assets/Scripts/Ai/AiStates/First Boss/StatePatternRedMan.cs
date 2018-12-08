using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StatePatternRedMan : StatePatternEnemy
{
    public List<GameObject> mirrorImageList;
    public GameObject mirrorImages;
    public int numberMirrorImages;
    public float delayTime = 3;
    public float healthPercentDifference = .15f;
    float lastHealthPercent = 0;
    
    public Health health;
    public void SetLastHealth()
    {
        lastHealthPercent = health.GetPercent();
    }
    public bool ReadyToMirror()
    {
        if(lastHealthPercent - health.GetPercent() >= healthPercentDifference && mirrorImageList.Count < numberMirrorImages)
        {
            return true;
        }
        return false;
    }

    public void RemoveEnemy(Health health)
    {
        mirrorImageList.RemoveAt(mirrorImageList.Count-1);
    }
}
