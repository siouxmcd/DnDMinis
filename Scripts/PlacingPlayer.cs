using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlacingPlayer : NetworkBehaviour
{
    private GameObject gameController;
    GameController controller;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
        float xPosition = Random.Range(55.5f, 58.5f);
        float zPosition = Random.Range(-44f, -41f);
        transform.position = new Vector3(xPosition, 52.69f, zPosition);
        AddPlayerToPlayerList();
	}

    private void AddPlayerToPlayerList()
    {
        controller.playerList.Add(gameObject);
        Debug.Log("Player loading");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
