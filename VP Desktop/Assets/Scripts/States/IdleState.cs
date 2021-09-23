using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.States
{
    public class IdleState : State
    {
        bool thinking = false;
        bool makeDecision = false;
        
        public override State RunState()
        {
            if(thinking) 
            {
                if(makeDecision) 
                {
                    makeDecision = false;
                    thinking = false;
                    return WhatNext();
                } 
            }
            else 
            {

                StartCoroutine(ShouldMakeDecision());
            }

            return this;
        }

        private IEnumerator ShouldMakeDecision()
        {
            thinking = true;
            yield return new WaitForSeconds(Random.Range(2, 5));
            makeDecision = true;
        }

        private State WhatNext()
        {
            if(DigimonManager.instance.CanPoop.Status)
            {
                return manager.GetState(DigimonStates.Poop);
            }
            if(DigimonManager.instance.IsHungry.Status)
            {
                return manager.GetState(DigimonStates.Hungry);
            }
            return manager.GetState(DigimonStates.Move);
        }
    }
}