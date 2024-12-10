using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInventory : MonoBehaviour
{
    public Sprite[] bigIcons;
    public Image bigIcon;
    public string[] titles;
    public Text title;
    public string[] descriptions;
    public Text description;
    public Button[] itemButtons;
    public GameObject useButton;
    public Text amtsText;

    private AudioSource audioPlayer;
    public AudioClip click, select;
    private int chosenItemNumber;

    private int updateHealth;
    private float updateStamina;
    private float updateInfection;

    private bool addHealth = false;
    private bool addStamina = false;
    private bool reduceInfection = false;

    public GameObject flashlightPanel, nightVisionPanel;
    private bool flashLightRefill = false;
    private bool nightVisionRefill = false;

    public GameObject electricDoorObj;
    public GameObject electricLight1, electricLight2;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        bigIcon.sprite = bigIcons[0];
        title.text = titles[0];
        description.text = descriptions[0];
        useButton.SetActive(false);
        electricLight1.SetActive(false);
        electricLight2.SetActive(false);
    }

    private void OnEnable()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (SaveScript.itemsPickedUp[i] == false)
            {
                itemButtons[i].image.color = new Color(1, 1, 1, 0.06f);
                itemButtons[i].image.raycastTarget = false;
            }
            if (SaveScript.itemsPickedUp[i] == true)
            {
                itemButtons[i].image.color = new Color(1, 1, 1, 1);
                itemButtons[i].image.raycastTarget = true;
            }
        }

        if(SaveScript.itemAmts[chosenItemNumber] <= 0)
        {
            ChooseItem(0);
        }
        ChooseItem(chosenItemNumber);
    }

    public void ChooseItem(int itemNumber)
    {
        bigIcon.sprite = bigIcons[itemNumber];
        title.text = titles[itemNumber];
        description.text = descriptions[itemNumber];
        if (audioPlayer != null)
        {
            audioPlayer.clip = click;
            audioPlayer.Play();
        }
        chosenItemNumber = itemNumber;
        amtsText.text = "Amts: " + SaveScript.itemAmts[itemNumber];

        if(itemNumber < 4)
        {
            useButton.SetActive(false);
        }
        else
        {
            useButton.SetActive(true);
        }
        if(itemNumber != 8)
        {
            flashLightRefill = false;
        }
        if (itemNumber != 9)
        {
            nightVisionRefill = false;
        }
    }

    public void AddHealth(int healthUpdate)
    {
        updateHealth = healthUpdate;
        addHealth = true;
    }

    public void AddStamina(int staminaUpdate)
    {
        updateStamina = staminaUpdate;
        addStamina = true;
    }

    public void ReduceInfection(int infectionUpdate)
    {
        updateInfection = infectionUpdate;
        reduceInfection = true;
    }

    public void FillFLBattery()
    {
        flashLightRefill = true;
    }

    public void FillNVBattery()
    {
        nightVisionRefill = true;
    }

    public void AssignItem()
    {
        SaveScript.itemID = chosenItemNumber;
        audioPlayer.clip = select;
        audioPlayer.Play();

        if (chosenItemNumber != 10 && chosenItemNumber != 11)
        {
            SaveScript.itemAmts[chosenItemNumber]--;
            ChooseItem(chosenItemNumber);
            if (SaveScript.itemAmts[chosenItemNumber] == 0)
            {
                SaveScript.itemsPickedUp[chosenItemNumber] = false;
                useButton.SetActive(false);
            }
        }


        if (addHealth == true)
        {
            addHealth = false;
            if (SaveScript.health < 100)
            {
                SaveScript.health += updateHealth;
            }
            if (SaveScript.health > 100)
            {
                SaveScript.health = 100;
            }
        }

        if (addStamina == true)
        {
            addStamina = false;
            if (SaveScript.stamina < 100)
            {
                SaveScript.stamina += updateStamina;
            }
            if (SaveScript.stamina > 100)
            {
                SaveScript.stamina = 100;
            }
        }

        if (reduceInfection == true)
        {
            reduceInfection = false;
            if (SaveScript.infection > 0.0f)
            {
                SaveScript.infection -= updateInfection;
            }
            if (SaveScript.infection < 0.0f)
            {
                SaveScript.infection = 0.0f;
            }
        }
        if(flashLightRefill == true)
        {
            flashLightRefill = false;
            flashlightPanel.GetComponent<FlashLightScript>().batteryPower = 1.0f;
        }
        if (nightVisionRefill == true)
        {
            nightVisionRefill = false;
            nightVisionPanel.GetComponent<NightVisionScript>().batteryPower = 1.0f;
        }

        if(chosenItemNumber == 10)
        {
            if(SaveScript.doorObject != null)
            {
                if((int)SaveScript.doorObject.GetComponent<DoorType>().chooseDoor == 1)
                {
                    if(SaveScript.doorObject.GetComponent<DoorType>().locked == true)
                    {
                        SaveScript.doorObject.GetComponent<DoorType>().locked = false;
                    }
                }
            }
        }

        if (chosenItemNumber == 11)
        {
            if (SaveScript.doorObject != null)
            {
                if ((int)SaveScript.doorObject.GetComponent<DoorType>().chooseDoor == 2)
                {
                    if (SaveScript.doorObject.GetComponent<DoorType>().locked == true)
                    {
                        SaveScript.doorObject.GetComponent<DoorType>().locked = false;
                    }
                }
            }
        }

        if(chosenItemNumber == 12)
        {
            if(SaveScript.generator != null)
            {
                SaveScript.generatorOn = true;
                SaveScript.generator.GetComponent<AudioSource>().Play();

                electricDoorObj.GetComponent<DoorType>().locked = false;
                electricLight1.SetActive(true);
                electricLight2.SetActive(true);
            }
        }

    }
}
