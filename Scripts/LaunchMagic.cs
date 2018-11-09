using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchMagic : MonoBehaviour
{

    public GameObject MagicBolt;

    public Transform Player;
    public Transform LaunchPosition;

    public int SpellSpeed;

    public bool CastSpell = true;
    public bool SpellPrepared = true;

    private int CastCounter = 0;
    private int CastRandomNumber = 0;

	// Use this for initialization
	void Start ()
    {
        CastRandomNumber = Random.Range(3, 20); // initial random casting number
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (Player != null)
        {
            LaunchPosition.transform.LookAt(Player);
        }

        if (CastSpell)
        {
            if (SpellPrepared)
            {
                if (CastCounter < CastRandomNumber)
                {
                    CastCounter++;
                }
                else
                {
                    Spell(MagicBolt);
                    SpellPrepared = false;
                }
            }
        }
	}

    private void Spell(GameObject Magic)
    {
        GameObject MagicSpell = Instantiate(Magic) as GameObject;

        MagicSpell.SetActive(true);
        MagicSpell.transform.position = LaunchPosition.position;
        MagicSpell.transform.rotation = LaunchPosition.rotation;

        MagicSpell.GetComponent<Rigidbody>().AddForce(LaunchPosition.forward * SpellSpeed * 1f, ForceMode.Impulse);

        StartCoroutine("PrepareSpell");
    }

    IEnumerator PrepareSpell()
    {
        yield return new WaitForSeconds(1f);
        CastRandomNumber = Random.Range(3, 20);
        SpellPrepared = true;
    }
}
