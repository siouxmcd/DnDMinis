using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonLanding : DragonState
{    
    public DragonLanding(DragonStateController dragonStateController) : base(dragonStateController) { }
    
    // Use this for initialization
    void Start ()
    {

    }

    public override void OnStateEnter()
    {
      
    }

    public override void CheckTransitions()
    {
        if (Vector3.Distance(dragonStateController.dragon.transform.position, dragonStateController.dragonLandingLocation.transform.position) < (dragonStateController.atNavPointDistance / 4))
        {
            dragonStateController.SetState(new DragonFighting(dragonStateController));
        }

        
    }
    public override void Act()
    {
        dragonStateController.dragon.transform.Translate(Vector3.forward * dragonStateController.dragonFlySpeed * Time.deltaTime);

        Vector3 dir = dragonStateController.dragonLandingLocation.transform.position - dragonStateController.dragon.gameObject.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        dragonStateController.transform.rotation = Quaternion.Slerp(dragonStateController.transform.rotation, rot, dragonStateController.dragonRotationSpeed * Time.deltaTime);
    }
    
    public override void OnStateExit() { }

    // Update is called once per frame
    void Update () {
		
	}
}
