using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SwitchOff", 1);
    }

    void SwitchOff()
    {
        this.gameObject.SetActive(false);
    }
}
