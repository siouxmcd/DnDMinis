using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour {

    private PhotonView pv;
    public GameObject myAvatar;

	// Use this for initialization
	void Start () {
        pv = GetComponent<PhotonView>();
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        if (pv.IsMine)
        {
            myAvatar =PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player3"), GameSetup.GS.spawnPoints[spawnPicker].position, GameSetup.GS.spawnPoints[spawnPicker].rotation, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
