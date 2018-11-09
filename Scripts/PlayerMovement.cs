using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{
    public float speed = 5;
    public float bulletSpeed = 2f;
    public GameObject bulletPrefab;
    public Transform LaunchPosition;
    private bool jumping = false;

	// Use this for initialization
	void Start ()
    {
        if (!isLocalPlayer)
        {
            GetComponentInChildren<SmoothMouseLookUp>().enabled = false;
            GetComponentInChildren<SmoothMouseLookAround>().enabled = false;
            GetComponentInChildren<PlayerMovement>().enabled = false;
            GetComponentInChildren<Camera>().enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime * -1;
        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.position += transform.right * speed * Time.deltaTime * -1;
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!jumping)
            {
                jumping = true;
                this.GetComponent<Rigidbody>().AddForce(transform.up * 5f, ForceMode.Impulse);
                StartCoroutine("JumpTimer");
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            CmdFireGun();
        }
    }

    IEnumerator JumpTimer()
    {
        yield return new WaitForSeconds(1.2f);
        jumping = false;
    }

    [Command]
    private void CmdFireGun()
    {
        if (NetworkServer.active)
        {
            var bullet = (GameObject)Instantiate(bulletPrefab, LaunchPosition.position, LaunchPosition.rotation);
     //       bullet.GetComponent<Rigidbody>().AddForce(LaunchPosition.forward * bulletSpeed * -1f, ForceMode.Impulse);
            NetworkServer.Spawn(bullet);
        }

    }
}
