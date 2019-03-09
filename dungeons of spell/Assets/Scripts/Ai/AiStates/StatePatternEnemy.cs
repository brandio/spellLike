using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour {

	public EnemyShoot shoot;

    public MonoBehaviour startState;
    IEnemyState currentState;
	[HideInInspector] public AiMovement movement;
    [HideInInspector] public Movement player;
    [HideInInspector] public float lastAttackTime = 0;
    [HideInInspector] public bool attack = false;
    [HideInInspector] public float timeBetweenAttacks = 2;

    public float AttackSpeed = 4;
    public float minAttackDistance = 0;
    public float maxAttackDistance = 0;
    public float minObserveDistance = 0;
    public Animator anim;
    public float numberOfAttacks = 1;
    int layersThatBlockAttack;

    public void ChangeState(IEnemyState state)
    {
        currentState.ExitState();
        currentState = state;
        currentState.EnterState();
    }
    public bool CanSeePlayer()
    {
        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.transform.position, layersThatBlockAttack);
        return (hit.collider == null);
    }

	private void Awake()
	{
		movement = this.GetComponent<AiMovement> ();
    }

    public Movement GetPlayer()
    {
        if(player != null)
        {
            return player;
        }
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        }
        return player;
    }

	// Use this for initialization
	void Start () {
        string[] layers = new string[1] { "BackGround" };
        layersThatBlockAttack = LayerMask.GetMask(layers);
        if (startState == null)
        {
            currentState = new ObserveState(this);
            currentState.EnterState();
        }
        else
        {
            IEnemyState startingState = startState as IEnemyState;
            currentState = startingState;
            currentState.EnterState();
        }
        anim.SetFloat("AttackSpeed", AttackSpeed);
        anim.SetFloat("MoveSpeed", movement.movementSpeed/2);
    }

    public void Attack()
    {
        shoot.shoot();
    }
    // Update is called once per frame
    void Update () 
	{
		currentState.UpdateState ();
        if(attack)
        {
            Attack();
            attack = false;
        }
	}
    	

}
