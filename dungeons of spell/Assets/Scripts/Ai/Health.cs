using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
    public float health = 40;

    public AudioClip deathSound;
    public AudioClip damageSound;
    AudioSource audioSource;

    public float experienceOnDeath = 10;

	public SpriteRenderer CharacterSprite;
    public bool ChromAbEffectOnDamage = false;

	public Animator anim;
    public delegate void EnemyDiedEventHandler(Health health);
    public event EnemyDiedEventHandler EnemyDied;

    public delegate void HealthChangedEventHandler(Health health);
    public event HealthChangedEventHandler HealthChanged;

    float maxHealth;
    float dmgCoolDown = 20;
    bool takeDamage = false;

    IEnumerator Death()
    {
		if (anim != null) {
			anim.SetBool("Death", true);
		}
        audioSource.clip = deathSound;
        audioSource.Play();
        gameObject.GetComponent<Collider2D>().enabled = false;
        StatePatternEnemy pattern = gameObject.GetComponent<StatePatternEnemy>();
        if (pattern != null)
        {
            pattern.enabled = false;
        }
        const float amount = 330f;
        int sign = 1;

        if (Random.Range(0, 10) > 5)
        {
            sign = -1;

        }
        for (float i = 0; i < 4; i++)
        {

            CharacterSprite.material.SetFloat("_OffsetBlueX", -sign * (i / amount));
            CharacterSprite.material.SetFloat("_OffsetBlueY", sign * (i / amount));
            CharacterSprite.material.SetFloat("_OffsetRedX", -sign * (i / amount));
            CharacterSprite.material.SetFloat("_OffsetRedY", -sign * (i / amount));
            CharacterSprite.material.SetFloat("_OffsetGreenX", sign * (i / amount));
            yield return new WaitForSeconds(.15f);
        }
        gameObject.SetActive(false);
    }

    public int Heal(int amount)
    {
        health += amount;
        int healAmount = amount;
        if(health >= maxHealth)
        {
			healAmount = amount - (int)Mathf.Round(health - maxHealth);
            health = maxHealth;
        }
        if(HealthChanged != null)
        {
            HealthChanged(this);
        }
        
        return healAmount;
    }

    public void TakeDamage(float damage)
	{
		takeDamage = false;

		if (dmgCoolDown > .2f) {
            ScreenShake.instance.shake(.02f);
            if (ChromAbEffectOnDamage)
            {
                StartCoroutine("ChromAbDamageAnimation");
            }
            else
            {
                StartCoroutine("GenericDamageAnimation");
            }
			
			if(audioSource)
			{
                audioSource.clip = damageSound;
                audioSource.Play ();
			}

			dmgCoolDown = 0;
		}

		health = health - damage;
		if (health < 0) {
            if (EnemyDied != null)
                EnemyDied(this);
            ScreenShake.instance.shake(.2f);
            if(ChromAbEffectOnDamage)
            {
                StartCoroutine("Death");
            }
            else
            {
                gameObject.SetActive(false);
            }
            
        }
        if (HealthChanged != null)
        {
            HealthChanged(this);
        }


    }
	// Use this for initialization
	void Start () {
        maxHealth = health;
		audioSource = this.GetComponent<AudioSource> ();
        audioSource.clip = damageSound;
        if (HealthChanged != null)
        {
            HealthChanged(this);
        }


        SpriteRenderer sprite = this.GetComponent<SpriteRenderer> ();
		if (sprite != null) {
			CharacterSprite = sprite;
		}
	}

    public float GetPercent()
    {
        return health / maxHealth;
    }
	IEnumerator GenericDamageAnimation()
	{
		const float amount = 1;
		Color orginalColor = CharacterSprite.color;
		for(float i = 0; i < amount; i++)
		{
			Color myColor = new Color(1,.3f ,.5f);
			CharacterSprite.color = myColor;
            yield return new WaitForSeconds(.1f);
            CharacterSprite.color = orginalColor;
            yield return new WaitForSeconds(.1f);
        }
        CharacterSprite.color = orginalColor;
    }

    IEnumerator ChromAbDamageAnimation()
    {
        Color orginalColor = CharacterSprite.color;
        const float amount = 90;
        int sign = 1;
        if(Random.Range(0,10) > 5)
        {
            sign = -1;
        }
        for (float i = 1; i >= 0; i--)
        {
            CharacterSprite.material.SetFloat("_OffsetBlueX", -sign * i / amount);
            CharacterSprite.material.SetFloat("_OffsetBlueY", sign * i / amount);
            CharacterSprite.material.SetFloat("_OffsetRedX", -sign * i / amount);
            CharacterSprite.material.SetFloat("_OffsetRedY", -sign * i / amount);
            CharacterSprite.material.SetFloat("_OffsetGreenX", sign * i / amount);
            yield return new WaitForSeconds(.2f);
        }
        CharacterSprite.color = orginalColor;
    }

    // Update is called once per frame
    void Update () {
		if (takeDamage) {
			TakeDamage(1);
		}
		dmgCoolDown = dmgCoolDown + Time.deltaTime;
	}

	IEnumerator DamageOverTime(float dmg, int ticks, float timeBetween)
	{
		for (int i = 0; i < ticks; i++) {
			TakeDamage(dmg);
			yield return new WaitForSeconds(timeBetween);
		}
	}

	public void TakeDamageOverTime(float dmg, int ticks, float timeBetween)
	{
		StartCoroutine(DamageOverTime(dmg,ticks,timeBetween));
	}
}
