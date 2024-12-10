using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    public GameObject winMessage;

    // Start is called before the first frame update
    void Start()
    {
        winMessage.SetActive(false); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(SaveScript.gotVaccine == true)
            {
                winMessage.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
