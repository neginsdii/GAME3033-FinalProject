using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieStates
{
	GameObject FollowTarget;
	const float StoppingDistance = 1.5f;
	int movementZHash = Animator.StringToHash("MovementZ");

	public ZombieFollowState(GameObject _followTarget, ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
	{
		FollowTarget = _followTarget;
		updateInterval = Random.Range(4, 10);
	}
	public override void Exit()
	{
		base.Exit();
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();
	}

	public override void IntervalUpdate()
	{
		base.IntervalUpdate();
		ownerZombie.ZombieNavMesh.SetDestination(FollowTarget.transform.position);

	}

	public override void Start()
	{
		base.Start();
		ownerZombie.ZombieNavMesh.SetDestination(FollowTarget.transform.position);
	}

	public override void Update()
	{
		base.Update();
		float moveZ = ownerZombie.ZombieNavMesh.velocity.normalized.z != 0f ? 1f : 0f;
		ownerZombie.zombieAnimator.SetFloat(movementZHash, moveZ);

		float DistanceBetween =Vector3.Distance(ownerZombie.transform.position, FollowTarget.transform.position);
		if(DistanceBetween<StoppingDistance)
		{
			stateMachine.Changestate(ZombieStateType.Attacking);
		}
		if (FollowTarget == null)
		{
			stateMachine.Changestate(ZombieStateType.Idle);
		}

	}
}
