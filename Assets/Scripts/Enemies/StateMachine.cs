using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public State currentState { get; private set; }
    protected Dictionary<ZombieStateType, State> states;
    bool isRunning;
	// Start is called before the first frame update
	private void Awake()
	{
        states = new Dictionary<ZombieStateType, State>();
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRunning)
		{
            currentState.Update();
		}
    }

    public void Initialize(ZombieStateType startingState)
    {
        if(states.ContainsKey(startingState))
		{
            Changestate(startingState);
		}
    }

    public void AddState(ZombieStateType stateName, State state)
	{
        if (states.ContainsKey(stateName)) return;
        states.Add(stateName, state);
	}
    public void RempveState(ZombieStateType stateName)
    {
        if (!states.ContainsKey(stateName)) return;
        states.Remove(stateName);
    }
    public void Changestate(ZombieStateType nextState)
    {
       if(isRunning)
		{
            StopRunningState();
		}
        if (!states.ContainsKey(nextState)) return;
        currentState = states[nextState];
        currentState.Start();
        if(currentState.updateInterval>0)
		{
            InvokeRepeating(nameof(IntervalUpdate), 0, currentState.updateInterval);
		}
        isRunning = true;
    }

    void StopRunningState()
	{
        isRunning = false;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
	}

    private void IntervalUpdate()
	{
        if (isRunning)
            currentState.IntervalUpdate();
	}
}
