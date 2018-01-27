using UnityEngine;
using System.Collections;

public class ProjectileCollision : MonoBehaviour {

	MovementCheck movementCheck;

	public delegate void BackGroundCollisionBehaviour(GameObject projectory, MovementCheck m);

	[HideInInspector]
	public bool collideWithBack = true;
	[HideInInspector]
	public BackGroundCollisionBehaviour backGroundCollisionBehaviour;

	public OnHitEffect.DamageBehaviour damageBehaviour;
    public SpellCollisionEffect.SpellCollisionBehaviour spellCollisionBehaviour;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public float priority;

    [HideInInspector]
    public GameObject caster;
    void Awake()
	{
		movementCheck = this.GetComponent<MovementCheck> (); 
	}
	
	// Update is called once per frame
	void Update () {
		if(!movementCheck.CheckMove (this.transform.position))
		{
			GameObject hitObject = movementCheck.HitObject(this.transform.position);

			if(LayerMask.NameToLayer("Enemy") == hitObject.layer || LayerMask.NameToLayer("Player") == hitObject.layer || LayerMask.NameToLayer("Destructable") == hitObject.layer)
			{
                damageBehaviour(hitObject, damage,caster);
                //hitObject.GetComponent<Health>().TakeDamage(damageBehaviour(hitObject,damage);
                gameObject.SetActive(false);
			}
			else if(LayerMask.NameToLayer("ProjectilePlayer") == hitObject.layer)
			{
				ProjectileDispute(hitObject.GetComponent<ProjectileCollision>());
			}
			else if(LayerMask.NameToLayer("ProjectileEnemy") == hitObject.layer)
			{

			}
			else if(LayerMask.NameToLayer("BackGround") == hitObject.layer)
			{
				if(collideWithBack)
				{
					backGroundCollisionBehaviour(gameObject,movementCheck);
				}
			}
			else
			{
				gameObject.SetActive(false);
			}
		}
	}

	void OnDisable  () {
		collideWithBack = true;
	}

	void ProjectileDispute(ProjectileCollision proj)
	{
		if (proj.gameObject.activeSelf && gameObject.activeSelf) {
			if (proj.priority  >= priority ) {
				proj.spellCollisionBehaviour( gameObject, proj.gameObject);
			}
			if (proj.priority  <= priority ) {
				spellCollisionBehaviour( proj.gameObject, gameObject);
            }
		}
	}
}
