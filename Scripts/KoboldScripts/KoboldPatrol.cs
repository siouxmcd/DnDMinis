using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldPatrol : KoboldState
{
    Transform destination;
    
	// Use this for initialization
	void Start ()
    {
        
    }

    public KoboldPatrol(KoboldStateController koboldStateController) : base(koboldStateController) { }

    public override void OnStateEnter()
    {
        Debug.Log(koboldStateController.ClosestPlayer);
        destination = koboldStateController.navPoint.transform;
        Animator anim = koboldStateController.warrior.GetComponent<Animator>();
        anim.Play("runNormal");
    }

    public override void CheckTransitions()
    {
        if (koboldStateController.CheckIfInRangeOfNavPoint())
        {
            koboldStateController.SetState(new KoboldAttack(koboldStateController));
        }
    }
    public override void Act()
    {
        koboldStateController.warrior.transform.Translate(Vector3.forward * koboldStateController.koboldSpeed * Time.deltaTime);

        Vector3 dir = destination.position - koboldStateController.warrior.gameObject.transform.position;
        Quaternion rot = Quaternion.LookRotation(dir);
        // slerp to the desired rotation over time
        koboldStateController.transform.rotation = Quaternion.Slerp(koboldStateController.transform.rotation, rot, koboldStateController.koboldRotationSpeed * Time.deltaTime);
    }
    public override void OnStateExit() { }

    // Update is called once per frame
    void Update () { }
}
