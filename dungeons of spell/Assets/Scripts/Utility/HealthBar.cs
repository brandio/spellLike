using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Health health;

    float startXScale;
    float startXPosition;

    float targetScale;
    Image image;
    // Use this for initialization
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        startXScale = transform.localScale.x;
        startXPosition = transform.localPosition.x;
        image.color = new Color(0, 1, .3f  );
        targetScale = startXPosition;
        health.HealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar(Health health)
    {
        targetScale = health.GetPercent() * startXScale;
    }
        

    public void Update()
    {
        MoveTowardsTargetHealth();
    }
    public void MoveTowardsTargetHealth()
    {
        if (targetScale < 0)
            return;
        float epsilon = 1;
        float amountToScale = 0;
        if(Mathf.Abs(targetScale - transform.localScale.x) > epsilon)
        {
            if (targetScale > transform.localScale.x)
            {
                amountToScale = 80 * Time.deltaTime;
            }
            else
            {
                amountToScale = -80 * Time.deltaTime;
            }
            
        }
        else
        {
            return;
        }

        image.color = new Color(1 - targetScale/ startXScale, targetScale / startXScale, .3f);
        transform.localScale = new Vector3(transform.localScale.x + amountToScale, transform.localScale.y, 1);
        transform.localPosition = new Vector3(startXPosition -  ((startXScale - transform.localScale.x)/2), transform.localPosition.y, 0);
    }
}

