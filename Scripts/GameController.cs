using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<GameObject> playerList = new List<GameObject>();
    public bool koboldScout01Alive = true;
    public bool dragonBossAlive = true;

    public GameObject rewardChest;

	// Use this for initialization
	void Start ()
    {
        rewardChest.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {


        if (!dragonBossAlive)
        {
            rewardChest.SetActive(true);
        }
	}
}
