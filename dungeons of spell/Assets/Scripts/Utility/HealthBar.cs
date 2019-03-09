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
        image.color = new Color(0, 1, .3f, minAlpha);
        targetScale = startXPosition;
        health.HealthChanged += UpdateHealthBar;
    }

    float fadeRate = .002f;
    float minAlpha = .1f;
    IEnumerator FaidHealth()
    {
        if(health.GetPercent() < .40f)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
        else
        {
            //Debug.Log("faidin");
            float i = minAlpha;
            while (i < 1)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, i);
                i = i + fadeRate * 10;
                yield return null;
            }
            while (i > minAlpha)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, i);
                i = i - fadeRate;
                yield return null;
            }
        }

    }

    public void UpdateHealthBar(Health health)
    {
        targetScale = health.GetPercent() * startXScale;
        StartCoroutine("FaidHealth");
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

        image.color = new Color(1 - targetScale/ startXScale - 0.3f, targetScale / startXScale, 1 - targetScale / startXScale);
        transform.localScale = new Vector3(transform.localScale.x + amountToScale, transform.localScale.y, 1);
        transform.localPosition = new Vector3(startXPosition -  ((startXScale - transform.localScale.x)/1.3f), transform.localPosition.y, 0);
    }
}

