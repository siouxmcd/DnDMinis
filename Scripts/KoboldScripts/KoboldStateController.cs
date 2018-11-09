using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldStateController : MonoBehaviour
{
    public KoboldState currentState;
    public GameObject warrior;
    public GameObject navPoint;

    public List<GameObject> players = new List<GameObject>();
    private int playerCounter = 0;
    public GameObject ClosestPlayer;
    public float ClosestDistance;

    public float koboldSpeed = 1.5f;
    public float koboldRotationSpeed = 2f;

    private GameObject gameController;
    public GameController controller;
    private GameObject playerGameObject;

    public bool alive = true;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
        navPoint = GameObject.Find("FighterWaypoint01");
        SetState(new KoboldPatrol(this));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (alive)
        {
            currentState.CheckTransitions();
            currentState.Act();

            if (players.Count < 4)
            {
                if (players.Count < controller.playerList.Count)
                {
                    playerGameObject = controller.playerList[playerCounter].gameObject;
                    players.Add(playerGameObject);
                    playerCounter++;
                }
            }
        }
        else
        {
            Animator anim = warrior.GetComponent<Animator>();
            anim.Play("death");
        }

    }

    public void SetState(KoboldState koboldState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = koboldState;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public bool CheckIfInRangeOfNavPoint()
    {
        Transform currentNavPoint = navPoint.transform;
        if (currentNavPoint != null)
        {
            if (Vector3.Distance(warrior.transform.position, currentNavPoint.position) < 2)
            {
                return true;
            }
        }
        return false;
    }

    public void DetermineClosestPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject TemporaryPlayerHolder = players[i].gameObject;
            float playerDistance = Vector3.Distance(TemporaryPlayerHolder.gameObject.transform.position, warrior.gameObject.transform.position);

            if (i == 0)
            {
                ClosestPlayer = TemporaryPlayerHolder;
                ClosestDistance = playerDistance;
            }
            else
            {
                if (playerDistance < ClosestDistance)
                {
                    ClosestPlayer = TemporaryPlayerHolder;
                    ClosestDistance = playerDistance;
                }
            }
        }
    }
}
