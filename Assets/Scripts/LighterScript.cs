using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighterScript : MonoBehaviour
{
    public GameObject lighterObj;

    // Start is called before the first frame update
    void OnEnable()
    {
        lighterObj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDisable()
    {
        lighterObj.SetActive(false);
    }
}
