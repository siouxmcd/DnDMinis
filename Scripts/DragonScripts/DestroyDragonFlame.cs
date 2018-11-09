using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDragonFlame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("DestroyFire");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DestroyFire()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
