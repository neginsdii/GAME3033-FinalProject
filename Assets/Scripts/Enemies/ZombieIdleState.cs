using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieStates
{
	int movementZHash = Animator.StringToHash("MovementZ");
	public ZombieIdleState(ZombieComponent zombie, StateMachine stateMachine) : base(zombie,stateMachine)
	{
	
	}
	public override void Exit()
	{
		base.Exit();
		ownerZombie.ZombieNavMesh.isStopped = false ;

	}


	public override void Start()
	{
		base.Start();
		ownerZombie.ZombieNavMesh.isStopped = true;
		ownerZombie.ZombieNavMesh.ResetPath();
		ownerZombie.zombieAnimator.SetFloat(movementZHash, 0);
	}


}
