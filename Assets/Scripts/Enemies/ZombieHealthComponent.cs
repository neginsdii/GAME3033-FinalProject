using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    StateMachine zombieStateMachine;
    public AudioSource audio;
    public bool isDead;
    private void Awake()
    {
        zombieStateMachine = GetComponent<StateMachine>();
        audio = GetComponent<AudioSource>();
        isDead = true;
    }
    // Start is called before the first frame update
    public override void Destroy()
    {
        if (isDead)
        {
            Data.numberOfZombies--;
            isDead = false;
        }
        zombieStateMachine.Changestate(ZombieStateType.IsDead);
            GetComponent<CapsuleCollider>().enabled = false;
        
    }
}
