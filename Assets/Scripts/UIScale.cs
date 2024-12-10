using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScale : MonoBehaviour
{
    public float scaleValue = 1;
    public float UHDScale = 2;

    // Start is called before the first frame update
    void Start()
    {
        if(Screen.width > 1920)
        {
            scaleValue = UHDScale;
        }
        this.transform.localScale = new Vector3(scaleValue, scaleValue, scaleValue);
    }
}
