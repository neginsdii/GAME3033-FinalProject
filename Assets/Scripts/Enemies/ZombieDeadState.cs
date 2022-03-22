using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : ZombieStates
{
	int movementZHash = Animator.StringToHash("MovementZ");
	int IsDeadHash = Animator.StringToHash("IsDead");
	public ZombieDeadState(ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
	{
	}
	public override void Exit()
	{
		base.Exit();
		ownerZombie.ZombieNavMesh.isStopped = false;

	}


	public override void Start()
	{
		base.Start();
		ownerZombie.ZombieNavMesh.isStopped = true;
		ownerZombie.ZombieNavMesh.ResetPath();
		ownerZombie.zombieAnimator.SetFloat(movementZHash, 0);
		ownerZombie.zombieAnimator.SetBool(IsDeadHash, true);
	}



}
