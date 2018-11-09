using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private GameObject gameController;
    public GameController controller;

    public List<GameObject> players = new List<GameObject>();
    private GameObject explosionPosition;
    private int playerCounter = 0;

    private GameObject playerGameObject;
    
    // Use this for initialization
    void Start ()
    {
        explosionPosition = GameObject.Find("Decal");
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();

        if (players.Count < 4)
        {
            if (players.Count < controller.playerList.Count)
            {
                playerGameObject = controller.playerList[playerCounter].gameObject;
                players.Add(playerGameObject);
                playerCounter++;
            }
        }

        for (int p = 0; p < players.Count; p++)
        {
            GameObject TemporaryPlayerHolder = players[p].gameObject;
            float playerDistance = Vector3.Distance(explosionPosition.transform.position, TemporaryPlayerHolder.gameObject.transform.position);


            if (playerDistance < 2)
            {
                TemporaryPlayerHolder.GetComponent<vp_FPPlayerDamageHandler>().Damage(1);
            }
        }
    }
 }
