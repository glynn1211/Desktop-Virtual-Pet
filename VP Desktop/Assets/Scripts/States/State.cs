using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State: MonoBehaviour
{
    protected StateMachine manager;

    public State Initalise(StateMachine manager)
    {
       this.manager = manager;
       return this;
    }

    public abstract State RunState();
}
