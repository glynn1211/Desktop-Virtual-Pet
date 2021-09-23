using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DigimonSpeed
{
    public int moveSpeed { get; private set; }
    public int spriteSpeed { get; private set; }

    public DigimonSpeed(int moveSpeed, int spriteSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.spriteSpeed = spriteSpeed;
    }
}

public class MoveState : State
{
    bool moving = false;
    bool finishedMove = false;

    private DigimonSpeed Movesettings = new DigimonSpeed(5,2);

    public override State RunState()
    {
        if(finishedMove)
        {
            finishedMove = false;
            return manager.GetState(DigimonStates.Idle);
        }

        if(!moving)
        {
            moving = true;
            Move();
        }

        return this;
    }

    public void Move()
    {
        DigimonManager.instance.MoveRandom(() => {        
           moving = false;
           finishedMove = true;
        });
    }
}
