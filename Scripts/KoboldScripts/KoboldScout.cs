using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldScout : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    private int playerCounter = 0;
    private GameObject gameController;
    public GameObject scout;
    public float scoutSpeed;
    public float scoutRotationSpeed;
    GameController controller;
    public GameObject ClosestPlayer;
    private float ClosestDistance;
    private bool playerSpotted = false;
    private bool playerWithinRange = false;
    public float AlertDistance = 10f;
    public Transform raycasterPosition;

    public GameObject[] koboldNavPoints;
    public int koboldNavPointNumber;
    public int koboldNavPointCounter;
    public Transform currentNavPoint;
    public float atNavPointDistance = 2f;
    Transform destination;

    private GameObject playerGameObject;

    public bool alive = true;

    // Use this for initialization
    void Start ()
    {
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (alive)
        {
            if (!playerSpotted)
            {
                if (players.Count < 4)
                {
                    if (players.Count < controller.playerList.Count)
                    {
                        playerGameObject = controller.playerList[playerCounter].gameObject;
                        players.Add(playerGameObject);
                        playerCounter++;
                    }
                }

                if (playerWithinRange)
                {
                    raycasterPosition.transform.LookAt(ClosestPlayer.transform.GetChild(2).position);
                    RaycastHit hit;
                    // Does the ray intersect any objects excluding the player layer
                    if (Physics.Raycast(raycasterPosition.transform.position, raycasterPosition.transform.TransformDirection(Vector3.forward), out hit))
                    {
                        if (hit.transform.gameObject.transform.parent.gameObject == ClosestPlayer)
                        {
                            playerSpotted = true;
                            Animator anim = scout.GetComponent<Animator>();
                            anim.Play("runNormal");
                            destination = GetNextNavPoint();
                        }
                    }
                }

                if (controller.playerList.Count != 0)
                {
                    DetermineClosestPlayer();
                }
            }
            else
            {
                if (destination.gameObject.name == "WaypointFinal" && CheckIfInRangeOfNavPoint(destination))
                {
                    controller.koboldScout01Alive = false;
                    Destroy(gameObject);
                }
                else if (destination == null || CheckIfInRangeOfNavPoint(destination))
                {
                    destination = GetNextNavPoint();
                    koboldNavPointCounter++;
                }
                else
                {
                    scout.transform.Translate(Vector3.forward * scoutSpeed * Time.deltaTime);

                    Vector3 dir = destination.position - scout.gameObject.transform.position;
                    Quaternion rot = Quaternion.LookRotation(dir);
                    // slerp to the desired rotation over time
                    transform.rotation = Quaternion.Slerp(transform.rotation, rot, scoutRotationSpeed * Time.deltaTime);
                }
            }
        }
        else
        {
            Animator anim = scout.GetComponent<Animator>();
            anim.Play("death");
        }
    }

    public void DetermineClosestPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject TemporaryPlayerHolder = players[i].gameObject;
            float playerDistance = Vector3.Distance(TemporaryPlayerHolder.gameObject.transform.position, scout.gameObject.transform.position);

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

        if (ClosestDistance < AlertDistance)
        {
            playerWithinRange = true;
        } else
        {
            playerWithinRange = false;
        }
    }

    public Transform GetNextNavPoint()
    {
        koboldNavPointNumber = (koboldNavPointNumber + 1) % koboldNavPoints.Length;
        return koboldNavPoints[koboldNavPointNumber].transform;
    }

    public bool CheckIfInRangeOfNavPoint(Transform navPoint)
    {
        currentNavPoint = koboldNavPoints[koboldNavPointNumber].transform;
        if (currentNavPoint != null)
        {
            if (Vector3.Distance(scout.transform.position, currentNavPoint.position) < atNavPointDistance)
            {
                return true;
            }
        }
        return false;
    }
}
