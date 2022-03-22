using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ZombieComponent : MonoBehaviour
{
    public float zombieDamage = 5;

    public NavMeshAgent ZombieNavMesh;
    public Animator zombieAnimator;
    public StateMachine stateMachine;
    public GameObject followTarget;

	private void Awake()
	{
        zombieAnimator = GetComponent<Animator>();
        ZombieNavMesh = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<StateMachine>();
    }
	// Start is called before the first frame update
	void Start()
    {
        Initialize(followTarget);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Initialize(GameObject _followTarget)
	{
        followTarget = _followTarget;
        ZombieIdleState idleState = new ZombieIdleState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.Idle, idleState);

        ZombieFollowState FollowState = new ZombieFollowState(followTarget,this, stateMachine);
        stateMachine.AddState(ZombieStateType.Following, FollowState);

        ZombieAttackState AttackState = new ZombieAttackState(followTarget,this, stateMachine);
        stateMachine.AddState(ZombieStateType.Attacking, AttackState);

        ZombieDeadState DeadState = new ZombieDeadState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.IsDead, DeadState);

        stateMachine.Initialize(ZombieStateType.Following);
    }
}
