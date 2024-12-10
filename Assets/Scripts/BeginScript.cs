using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    public void Begin()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Destroy(gameObject);
    }


}
