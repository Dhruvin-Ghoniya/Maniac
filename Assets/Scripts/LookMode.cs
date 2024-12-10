using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LookMode : MonoBehaviour
{
    private PostProcessVolume vol;
    public PostProcessProfile standard;
    public PostProcessProfile nightVision;
    public PostProcessProfile inventory;
    public GameObject nightVisionOverlay;
    public GameObject flashlightOverlay;
    public GameObject inventoryMenu;
    public GameObject combinePanel;
    private Light flashLight;
    private bool nightVisionOn = false;
    private bool flashLightOn = false;
    public GameObject pointer;

    // Start is called before the first frame update
    void Start()
    {
        vol = GetComponent<PostProcessVolume>();
        flashLight = GameObject.Find("FlashLight").GetComponent<Light>();
        flashLight.enabled = false;
        nightVisionOverlay.SetActive(false);
        flashlightOverlay.SetActive(false);
        inventoryMenu.SetActive(false);
        vol.profile = standard;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            if (SaveScript.inventoryOpen == false)
            {
                if (nightVisionOn == false)
                {
                    vol.profile = nightVision;
                    nightVisionOverlay.SetActive(true);
                    nightVisionOn = true;
                    NightVisionOff();
                }
                else if (nightVisionOn == true)
                {
                    vol.profile = standard;
                    nightVisionOverlay.SetActive(false);
                    nightVisionOverlay.GetComponent<NightVisionScript>().StopDrain();
                    this.gameObject.GetComponent<Camera>().fieldOfView = 60;
                    nightVisionOn = false;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if (SaveScript.inventoryOpen == false)
            {
                if (flashLightOn == false)
                {
                    flashlightOverlay.SetActive(true);
                    flashLight.enabled = true;
                    flashLightOn = true;
                    FlashLightSwitchOff();
                }
                else if (flashLightOn == true)
                {
                    flashlightOverlay.SetActive(false);
                    flashLight.enabled = false;
                    flashlightOverlay.GetComponent<FlashLightScript>().StopDrain();
                    flashLightOn = false;
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            if(SaveScript.inventoryOpen == false)
            {
                vol.profile = inventory;
                inventoryMenu.SetActive(true);

                if(flashLightOn == true)
                {
                    flashlightOverlay.SetActive(false);
                    flashLight.enabled = false;
                    flashlightOverlay.GetComponent<FlashLightScript>().StopDrain();
                    flashLightOn = false;
                }
                if(nightVisionOn == true)
                {
                    nightVisionOverlay.SetActive(false);
                    nightVisionOverlay.GetComponent<NightVisionScript>().StopDrain();
                    this.gameObject.GetComponent<Camera>().fieldOfView = 60;
                    nightVisionOn = false;
                }
            }
            else if (SaveScript.inventoryOpen == true)
            {
                vol.profile = standard;
                combinePanel.SetActive(false);
                inventoryMenu.SetActive(false);
            }
        }

        if(nightVisionOn == true)
        {
            NightVisionOff();
        }

        if(flashLightOn == true)
        {
            FlashLightSwitchOff();
        }

        if(SaveScript.inventoryOpen == true || Time.timeScale == 0)
        {
            Cursor.visible = true;
            pointer.SetActive(false);
        }
        else
        {
            Cursor.visible = false;
            pointer.SetActive(true);
        }
    }

    private void NightVisionOff()
    {
        if(nightVisionOverlay.GetComponent<NightVisionScript>().batteryPower <= 0)
        {
            vol.profile = standard;
            nightVisionOverlay.SetActive(false);
            this.gameObject.GetComponent<Camera>().fieldOfView = 60;
            nightVisionOn = false;
        }
    }

    private void FlashLightSwitchOff()
    {
        if(flashlightOverlay.GetComponent<FlashLightScript>().batteryPower <= 0)
        {
            flashlightOverlay.SetActive(false);
            flashLight.enabled = false;
            flashlightOverlay.GetComponent<FlashLightScript>().StopDrain();
            flashLightOn = false;
        }
    }
    
}
