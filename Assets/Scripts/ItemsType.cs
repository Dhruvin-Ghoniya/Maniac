using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsType : MonoBehaviour
{
    public enum typeOfItem
    {
        flashlight,
        nightvision,
        lighter,
        rags,
        healthPack,
        pills,
        waterBottle,
        apple,
        flashlightBattery,
        nightvisionBattery,
        houseKey,
        cabinKey,
        jerryCan
    }

    public typeOfItem chooseItem;
   
}
