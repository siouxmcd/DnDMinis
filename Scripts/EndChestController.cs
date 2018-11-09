using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndChestController : MonoBehaviour
{
    private GameObject gameController;
    public GameController controller;

    public List<GameObject> players = new List<GameObject>();
    private int playerCounter = 0;

    private GameObject playerGameObject;
    public GameObject ClosestPlayer;
    private float ClosestDistance = 50f;

    public GameObject chest;
    public float OpenDistance = 5f;

    private bool chestOpen = false;

    // Use this for initialization
    void Start()
    {
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
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

        if (controller.playerList.Count != 0)
        {
            DetermineClosestPlayer();
        }

        if (ClosestDistance < OpenDistance)
        {
            if (!chestOpen)
            {
                chestOpen = true;
                chest.GetComponent<Animation>().Play("woodenchest_large_open");
            }            
        }
    }

    public void DetermineClosestPlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            GameObject TemporaryPlayerHolder = players[i].gameObject;
            float playerDistance = Vector3.Distance(TemporaryPlayerHolder.gameObject.transform.position, chest.gameObject.transform.position);

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