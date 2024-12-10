using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySwitch : MonoBehaviour
{
    public GameObject weaponPanel, itemsPanel, combinePanel;

    // Start is called before the first frame update
    void Start()
    {
        weaponPanel.SetActive(true);
        itemsPanel.SetActive(false);
    }

    public void SwitchItemsOn()
    {
        weaponPanel.SetActive(false);
        itemsPanel.SetActive(true);
        combinePanel.SetActive(false);
    }

    public void SwitchWeaponsOn()
    {
        weaponPanel.SetActive(true);
        itemsPanel.SetActive(false);
        combinePanel.SetActive(false);
    }
}
