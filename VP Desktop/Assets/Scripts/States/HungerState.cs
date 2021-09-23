using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerState : State
{
    bool hunger = false;
    public override State RunState()
    {
        hunger = !DigimonManager.instance.IsHungry.Status;
        if(!hunger)
        {
            int food = DigimonManager.instance.CheckhungerLevel();
            if(food != 0)
            {
                //DO A THING
                ShowHungerEmote();
            }
            else
            {
                DigimonManager.instance.AddCareMistake();

            }
            DigimonManager.instance.IsHungry.Reset();
            return manager.GetState(DigimonStates.Idle);
        }
        return this;
    }

    private void ShowHungerEmote()
    {
        DigimonManager.instance.ShowHungerEmote();
    }
}
