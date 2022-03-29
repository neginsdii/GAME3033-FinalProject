using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    StateMachine zombieStateMachine;

    private void Awake()
    {
        zombieStateMachine = GetComponent<StateMachine>();
    }
    // Start is called before the first frame update
    public override void Destroy()
    {
        zombieStateMachine.Changestate(ZombieStateType.IsDead);
    }
}
