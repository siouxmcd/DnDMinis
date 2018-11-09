using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class DragonState
{
    protected DragonStateController dragonStateController;

    public abstract void CheckTransitions();
    public abstract void Act();
    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public DragonState(DragonStateController dragonStateController)
    {
        this.dragonStateController = dragonStateController;
    }
}
