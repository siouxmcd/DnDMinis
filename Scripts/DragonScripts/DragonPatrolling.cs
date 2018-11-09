using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragonPatrolling : DragonState
{
    Transform destination;
    private GameObject gameController;
    GameController controller;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
    }

    public DragonPatrolling(DragonStateController dragonStateController) : base(dragonStateController) { }

    public override void CheckTransitions()
    {
        if (dragonStateController.dragonNavPointNumber >= 53 && dragonStateController.dragonNavPointNumber <= 85)
        {           
            for (int i = 0; i < dragonStateController.players.Count; i++)
            {
                GameObject TemporaryPlayerHolder = dragonStateController.players[i].gameObject;
                if (TemporaryPlayerHolder.transform.position.x <= -26 && TemporaryPlayerHolder.transform.position.x >= -100)
                {
                    if (TemporaryPlayerHolder.transform.position.y >= 5.3 && TemporaryPlayerHolder.transform.position.y <= 20)
                    {
                        if (TemporaryPlayerHolder.transform.position.z >= -29 && TemporaryPlayerHolder.transform.position.z <= 51)
                        {
                            dragonStateController.SetState(new DragonLanding(dragonStateController));
                        }
                    }
                }
            }            
        }
    }

    public override void Act()
    {
        if (destination == null || dragonStateController.CheckIfInRangeOfNavPoint(destination))
        {
            destination = dragonStateController.GetNextNavPoint();
            dragonStateController.dragonNavPointCounter++;

        }
        else
        {
            dragonStateController.dragon.transform.Translate(Vector3.forward * dragonStateController.dragonFlySpeed * Time.deltaTime);
            
            Vector3 dir = destination.position - dragonStateController.dragon.gameObject.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            dragonStateController.transform.rotation = Quaternion.Slerp(dragonStateController.transform.rotation, rot, dragonStateController.dragonRotationSpeed * Time.deltaTime);
        }
    }

    public override void OnStateEnter()
    {
        destination = dragonStateController.GetNextNavPoint();
        dragonStateController.dragon.gameObject.transform.LookAt(destination);
    }

    public override void OnStateExit() { }


	
	// Update is called once per frame
	void Update () {
		
	}
}
