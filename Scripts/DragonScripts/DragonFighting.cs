using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragonFighting : DragonState 
{    
    private int outOfDragonRoomCount;
    private int inDragonRoomCount;

    private float flameAttackTime = 2f;
    private float flameTimer;

    public DragonFighting(DragonStateController dragonStateController) : base(dragonStateController) { }

    // Use this for initialization
    void Start()
    {
        
    }

    public override void OnStateEnter()
    {
        dragonStateController.dragon.GetComponent<vp_DamageHandler>().enabled = true;
        dragonStateController.dragon.transform.position = dragonStateController.dragonLandingLocation.transform.position;
        Animator anim = dragonStateController.dragon.GetComponent<Animator>();
        anim.Play("idleLookAround");
    }

    public override void CheckTransitions()
    {
        if (dragonStateController.alive)
        {
            inDragonRoomCount = 0;
            outOfDragonRoomCount = 0;

            for (int i = 0; i < dragonStateController.players.Count; i++)
            {
                GameObject TemporaryPlayerHolder = dragonStateController.players[i].gameObject;
                if (TemporaryPlayerHolder.transform.position.x <= -26 && TemporaryPlayerHolder.transform.position.x >= -100)
                {
                    if (TemporaryPlayerHolder.transform.position.y >= 5.3 && TemporaryPlayerHolder.transform.position.y <= 20)
                    {
                        if (TemporaryPlayerHolder.transform.position.z >= -29 && TemporaryPlayerHolder.transform.position.z <= 51)
                        {
                            inDragonRoomCount++;
                        }
                        else
                        {
                            outOfDragonRoomCount++;
                        }
                    }
                    else
                    {
                        outOfDragonRoomCount++;
                    }
                }
                else
                {
                    outOfDragonRoomCount++;
                }
            }

            if (inDragonRoomCount == dragonStateController.players.Count)
            {

            }
            else if (inDragonRoomCount < dragonStateController.players.Count && inDragonRoomCount >= 1)
            {

            }
            else if (inDragonRoomCount == 0 && outOfDragonRoomCount >= 1)
            {
                dragonStateController.dragonNavPointNumber = 74;
                dragonStateController.dragonNavPointCounter = 73;
                dragonStateController.SetState(new DragonPatrolling(dragonStateController));
            }
        }
        else
        {
            dragonStateController.controller.dragonBossAlive = false;
            Animator anim = dragonStateController.dragon.GetComponent<Animator>();
            anim.Play("death");
        }
        
    }

    public override void Act()
    {
        dragonStateController.DetermineClosestPlayer();

        Vector3 dir = dragonStateController.ClosestPlayer.gameObject.transform.position - dragonStateController.dragon.gameObject.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        dragonStateController.transform.rotation = Quaternion.Slerp(dragonStateController.transform.rotation, rot, dragonStateController.dragonRotationSpeed * Time.deltaTime);

        if (flameAttackTime < Time.time - flameTimer)
        {
            dragonStateController.CmdFire();
            flameTimer = Time.time;
        }

    }

    public override void OnStateExit()
    {
        Animator anim = dragonStateController.dragon.GetComponent<Animator>();
        anim.Play("flyNormal");
    }
    
	// Update is called once per frame
	void Update ()
    {
    }
}
