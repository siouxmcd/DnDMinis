using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldSpawner : MonoBehaviour
{
    private GameObject gameController;
    GameController controller;
    public GameObject koboldFighterPrefab;
    public GameObject area01SpawnLocation;

    private float koboldLastSpawnTime;
    private float timeBetweenKoboldSpawns;
    private int koboldsToSpawnAtLocation01 = 5;

	// Use this for initialization
	void Start ()
    {
        gameController = GameObject.Find("GameController");
        controller = gameController.GetComponent<GameController>();
        timeBetweenKoboldSpawns = Random.Range(1f, 5f);
        koboldLastSpawnTime = Time.time;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (controller.koboldScout01Alive == false)
        {
            if (koboldsToSpawnAtLocation01 > 0)
            {
                if (timeBetweenKoboldSpawns < Time.time - koboldLastSpawnTime)
                {
                    Debug.Log("In the placing kobold");
                    koboldsToSpawnAtLocation01--;
                    GameObject koboldWarrior = Instantiate(koboldFighterPrefab) as GameObject;
                    koboldWarrior.transform.position = area01SpawnLocation.transform.position;
                    koboldWarrior.transform.rotation = area01SpawnLocation.transform.rotation;
                    koboldLastSpawnTime = Time.time;
                    timeBetweenKoboldSpawns = Random.Range(1f, 5f);
                }
            }
        }
	}
}
