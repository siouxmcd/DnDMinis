using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMagic : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        StartCoroutine("DestroySpell");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Scenery")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySpell()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
