using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    private bool canDamage = false;
    private Collider col;
    private Animator bloodEffect;
    private AudioSource hitSound;
    public int damageAmt = 3;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        bloodEffect = GameObject.Find("Blood").GetComponent<Animator>();
        hitSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(col.enabled == false)
        {
            canDamage = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(canDamage == true)
            {
                canDamage = false;
                if (SaveScript.health > 0)
                {
                    SaveScript.health -= damageAmt;
                }
                if (SaveScript.infection < 100)
                {
                    SaveScript.infection += damageAmt;
                }
                bloodEffect.SetTrigger("blood");
                hitSound.Play();
            }
        }
    }
}
