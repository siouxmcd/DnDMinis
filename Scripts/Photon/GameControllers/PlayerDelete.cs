using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDelete : MonoBehaviour {

    private PhotonView pv;

    public GameObject player;

	// Use this for initialization
	void Start () {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
            return;
        Instantiate(player, transform.position, Quaternion.identity);
        player.transform.parent = this.gameObject.transform;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
