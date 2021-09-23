using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopState : State
{
    bool pooping = false;
    bool finished = false;
    public override State RunState()
    {
        if(finished)
        {
            DigimonManager.instance.CanPoop.Reset();
            finished = false;
            return manager.GetState(DigimonStates.Idle);
        }
        if(!pooping)
        {
            pooping = true;
            StartCoroutine(StartPooping());
        }

        return this;
    }

    IEnumerator StartPooping()
    {
        yield return new WaitForSeconds(5);
        GameObject poop = Instantiate(Resources.Load<GameObject>("poop"), this.gameObject.transform);
        if(DigimonManager.instance.sprite.transform.localScale.x > 0)
        {
            poop.transform.localPosition = new Vector3(0, poop.transform.localPosition.y, poop.transform.localPosition.z);
        }
        if(poop.transform.parent != null)
        {
            poop.transform.SetParent(poop.transform.parent.parent, true);
        }
        finished = true;
        pooping = false;
    }
}
