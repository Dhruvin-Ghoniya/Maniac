using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightVisionScript : MonoBehaviour
{
    private Image zoomBar;
    private Image batteryChunks;
    private Camera cam;
    public float batteryPower = 1.0f;
    public float drainTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        zoomBar = GameObject.Find("ZoomBar").GetComponent<Image>();
        batteryChunks = GameObject.Find("BatteryChunks").GetComponent<Image>();
        cam = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        InvokeRepeating("BatteryDrain", drainTime, drainTime);
        if (zoomBar != null)
        zoomBar.fillAmount = 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if(cam.fieldOfView > 10)
            {
                cam.fieldOfView -= 5;
                zoomBar.fillAmount = cam.fieldOfView / 100;
            }           
        }
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.fieldOfView < 60)
            {
                cam.fieldOfView += 5;
                zoomBar.fillAmount = cam.fieldOfView / 100;
            }
        }
        batteryChunks.fillAmount = batteryPower;
    }

    private void BatteryDrain()
    {
        if (batteryPower > 0.0f)
            batteryPower -= 0.25f;
    }

    public void StopDrain()
    {
        CancelInvoke("BatteryDrain");
    }
}
