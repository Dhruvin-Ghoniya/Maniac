using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupVaccine : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(SaveScript.vaccine != null)
            {
                SaveScript.gotVaccine = true;
                Destroy(gameObject);
            }
        }
    }
}
