using Assets.Scripts.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StateMachine : MonoBehaviour
{
    private State currentState;
    private Dictionary<DigimonStates, State> behaviours = new Dictionary<DigimonStates, State>();

    public void SetState(DigimonStates state)
    {
        if(behaviours.ContainsKey(state))
        {
            currentState = behaviours[state];
        }
    }

    public State GetState(DigimonStates state) 
    {    
        return behaviours[state];   
    }

    public void Initalise()
    {
        behaviours = new Dictionary<DigimonStates, State>() {
            { DigimonStates.Idle, gameObject.AddComponent<IdleState>().Initalise(this) },
            { DigimonStates.Move, gameObject.AddComponent<MoveState>().Initalise(this) },
            { DigimonStates.Poop, gameObject.AddComponent<PoopState>().Initalise(this) },
            { DigimonStates.Hungry, gameObject.AddComponent<HungerState>().Initalise(this)}
        };

        SetState(DigimonStates.Idle);
    }

    private void Update()
    {
        State next = currentState.RunState();

        if(next != currentState)
        {
            currentState = next;
        }
    }
}
