using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DragonStateController : NetworkBehaviour
{
    private GameObject gameController;
    public GameController controller;    

    public DragonState currentState;
    public GameObject[] dragonNavPoints;
    public GameObject dragonLandingLocation;
    public GameObject dragon;
    public GameObject dragonLaunchPosition;
    public GameObject firePrefab;
    public GameObject ClosestPlayer;
    private float ClosestDistance;
    public List<GameObject> players = new List<GameObject>();
    private int playerCounter = 0;
    public int dragonNavPointNumber;
    public int dragonNavPointCounter;
    public Transform currentNavPoint;
    public float dragonFlySpeed;
    public float dragonFireSpeed;
    public float dragonRotationSpeed;    
    public float atNavPointDistance = 2f;
    public int dragonHealth = 200;

    private GameObject playerGameObject;

    public bool alive = true;

    // Use this for initialization
    void Start ()
    {
        SetState(new DragonPatrolling(this));
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
        dragon.GetComponent<vp_DamageHandler>().enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
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

    public void SetState(DragonState dragonState)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = dragonState;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void DetermineClosestPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject TemporaryPlayerHolder = players[i].gameObject;
            float playerDistance = Vector3.Distance(TemporaryPlayerHolder.gameObject.transform.position, dragon.gameObject.transform.position);

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

    public Transform GetNextNavPoint()
    {
        dragonNavPointNumber = (dragonNavPointNumber + 1) % dragonNavPoints.Length;
        return dragonNavPoints[dragonNavPointNumber].transform;
    }

    public bool CheckIfInRangeOfNavPoint(Transform navPoint)
    {
        currentNavPoint = dragonNavPoints[dragonNavPointNumber].transform;
        if (currentNavPoint != null)
        {
            if (Vector3.Distance(dragon.transform.position, currentNavPoint.position) < atNavPointDistance)
            {
                return true;
            }
        }
        return false;
    }

    [Command]
    public void CmdFire()
    {
        dragonLaunchPosition.gameObject.transform.LookAt(ClosestPlayer.gameObject.transform.position);
        var flame = (GameObject)Instantiate(firePrefab, dragonLaunchPosition.gameObject.transform.position, dragonLaunchPosition.gameObject.transform.rotation);
    //    flame.GetComponent<Rigidbody>().AddForce(dragonLaunchPosition.gameObject.transform.forward * dragonFireSpeed * -1f, ForceMode.Impulse);
        NetworkServer.Spawn(flame);
    }
}
