using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldAttack : KoboldState
{
    Transform destination;
    private float attackRange = 0.4f;
    private float attackTimer = 4f;
    private float completeAttackTimer = 1.5f;
    private float lastAttackTime;
    private bool waitingToAttack = false;
    private bool attackComplete = true;
    private bool currentlyMoving = true;
    private bool damageDealt = true;
    private float lastDamageTime;
    private float damagePauseTime = 0.85f;
    // Use this for initialization
    void Start() { }

    public KoboldAttack(KoboldStateController koboldStateController) : base(koboldStateController) { }

    public override void OnStateEnter()
    {
        Debug.Log("attack");
        koboldStateController.DetermineClosestPlayer();
        lastAttackTime = Time.time;
    }
    public override void CheckTransitions()
    {

    }
    public override void Act()
    {
        if (!damageDealt)
        {
            if (damagePauseTime < Time.time - lastDamageTime)
            {
                
                koboldStateController.ClosestPlayer.GetComponent<vp_FPPlayerDamageHandler>().Damage(2);
                damageDealt = true;
            }
        }
        else
        {
            koboldStateController.DetermineClosestPlayer();
            destination = koboldStateController.ClosestPlayer.gameObject.transform;
        }

        if (koboldStateController.ClosestDistance > attackRange)
        {
            if (!currentlyMoving)
            {
                Animator ani = koboldStateController.warrior.GetComponent<Animator>();
                ani.Play("runNormal");
                currentlyMoving = true;
            }
            koboldStateController.warrior.transform.Translate(Vector3.forward * koboldStateController.koboldSpeed * Time.deltaTime);

            Vector3 dir = destination.position - koboldStateController.warrior.gameObject.transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            // slerp to the desired rotation over time
            koboldStateController.transform.rotation = Quaternion.Slerp(koboldStateController.transform.rotation, rot, koboldStateController.koboldRotationSpeed * Time.deltaTime);
        }
        else
        {
            currentlyMoving = false;
            if (attackTimer > Time.time - lastAttackTime)
            {
                if (!waitingToAttack && attackComplete)
                {
                    Animator anim = koboldStateController.warrior.GetComponent<Animator>();
                    anim.Play("idleCombat0");
                    waitingToAttack = true;
                }
                else
                {
                    if (completeAttackTimer < Time.time - lastAttackTime)
                    {
                        attackComplete = true;
                    }
                }
            }
            else
            {
                Animator anima = koboldStateController.warrior.GetComponent<Animator>();
                anima.Play("4HitCombo");
                lastAttackTime = Time.time;
                attackComplete = false;
                waitingToAttack = false;
                damageDealt = false;
                lastDamageTime = Time.time;                
            }
        }
    }
    public override void OnStateExit() { }

    // Update is called once per frame
    void Update() { }


}
