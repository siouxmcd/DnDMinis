using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
 //   DragonStateController dragon;


	// Use this for initialization
	void Start ()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * 2 * -1f, ForceMode.Impulse);
        StartCoroutine("DestroyBullet");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Dragon")
        {
            Debug.Log("Hit the dragon");
            Destroy(gameObject);
        }

        Debug.Log(col.gameObject.tag);
        Destroy(gameObject);
    }
}
