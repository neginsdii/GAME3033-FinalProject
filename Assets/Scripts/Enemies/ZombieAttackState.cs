using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackState : ZombieStates
{
	GameObject followTarget;
	float attackRange = 2;

	private IDamageable damageableObject;

	int movementZHash = Animator.StringToHash("MovementZ");
	int IsAttackingHash = Animator.StringToHash("IsAttacking");
	public ZombieAttackState(GameObject _followTarget, ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
	{
		followTarget = _followTarget;
		updateInterval = 2;
		damageableObject = followTarget.GetComponent<IDamageable>();
	}
	public override void Exit()
	{
		base.Exit();
		ownerZombie.ZombieNavMesh.isStopped = false;
		ownerZombie.zombieAnimator.SetBool(IsAttackingHash, false);

	}

	

	public override void IntervalUpdate()
	{
		base.IntervalUpdate();
		damageableObject?.TakeDamage(ownerZombie.zombieDamage);
	}

	public override void Start()
	{
		ownerZombie.ZombieNavMesh.isStopped = true;
		ownerZombie.ZombieNavMesh.ResetPath();
		ownerZombie.zombieAnimator.SetFloat(movementZHash, 0);
		ownerZombie.zombieAnimator.SetBool(IsAttackingHash, true); 
	}

	public override void Update()
	{
		if (followTarget == null)
		{
			stateMachine.Changestate(ZombieStateType.Idle);
		}
		else
		{
			ownerZombie.transform.LookAt(followTarget.transform.position, Vector3.up);
			float DistanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
			if(!ownerZombie.GetComponent<AudioSource>().isPlaying)
			ownerZombie.GetComponent<AudioSource>().Play();
			if (DistanceBetween > attackRange)
			{
				stateMachine.Changestate(ZombieStateType.Following);
			}
		}
	}
}
