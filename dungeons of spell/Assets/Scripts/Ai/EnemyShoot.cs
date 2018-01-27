using UnityEngine;
using System.Collections;

public class EnemyShoot : MonoBehaviour {
    [HideInInspector]
	public SpellBook current_spell;
    [HideInInspector]
    public float coolDown;
    float lastShootTime;
	AudioSource audioSource;
    public bool parentsRotation = true;
    public delegate void ShootEventHandler();
    public event ShootEventHandler ShotFired;
    void OnDisable()
    {
        if(current_spell == null)
        {
            return;
        }
        ProjectileSpellBook book = (ProjectileSpellBook) current_spell;
        book.ReturnProj();
    }

    void Start()
	{
        lastShootTime = Time.time;
        audioSource = this.GetComponent<AudioSource>();
    }

	public void shoot()
	{
        if(Time.time - lastShootTime < coolDown)
        {
            return;
        }
        lastShootTime = Time.time;
        if(ShotFired != null)
        {
            ShotFired();
        }

        //ScreenShake.instance.shake (.001f * current_spell.mana);  
        audioSource.Play ();
        current_spell.Cast (transform, parentsRotation);
	}

}
