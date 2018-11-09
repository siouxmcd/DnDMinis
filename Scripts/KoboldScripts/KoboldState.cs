using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KoboldState
{
    protected KoboldStateController koboldStateController;

    public abstract void CheckTransitions();
    public abstract void Act();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public KoboldState(KoboldStateController koboldStateController)
    {
        this.koboldStateController = koboldStateController;
    }

}
