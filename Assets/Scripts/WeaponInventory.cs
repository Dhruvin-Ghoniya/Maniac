using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventory : MonoBehaviour
{
    public Sprite[] bigIcons;
    public Image bigIcon;
    public string[] titles;
    public Text title;
    public string[] descriptions;
    public Text description;
    public Button[] weaponButtons;
    public Text amtsText;

    private AudioSource audioPlayer;
    public AudioClip click, select;
    private int chosenWeaponNumber;

    public GameObject useButton, combineButton;
    public GameObject combinePanel, combineUseButton;
    public Image[] combineItems;
    public GameObject sprayPanel;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();

        bigIcon.sprite = bigIcons[0];
        title.text = titles[0];
        description.text = descriptions[0];
        amtsText.text = "Amts: 1";
        combinePanel.SetActive(false);
        combineButton.SetActive(false);
    }

    private void OnEnable()
    {
        for(int i = 0; i < weaponButtons.Length; i++)
        {
            if(SaveScript.weaponsPickedUp[i] == false)
            {
                weaponButtons[i].image.color = new Color(1, 1, 1, 0.06f);
                weaponButtons[i].image.raycastTarget = false;
            }
            if (SaveScript.weaponsPickedUp[i] == true)
            {
                weaponButtons[i].image.color = new Color(1, 1, 1, 1);
                weaponButtons[i].image.raycastTarget = true;
            }
        }

        if(chosenWeaponNumber < 6)
        {
            combinePanel.SetActive(false);
            combineButton.SetActive(false);
        }

        if(SaveScript.itemsPickedUp[2] == true)
        {
            combineItems[0].color = new Color(1, 1, 1, 1);
        }
        if (SaveScript.itemsPickedUp[2] == false)
        {
            combineItems[0].color = new Color(1, 1, 1, 0.06f);
        }
        if (SaveScript.itemsPickedUp[3] == true)
        {
            combineItems[1].color = new Color(1, 1, 1, 1);
        }
        if (SaveScript.itemsPickedUp[3] == false)
        {
            combineItems[1].color = new Color(1, 1, 1, 0.06f);
        }

        if(SaveScript.weaponAmts[chosenWeaponNumber] <= 0)
        {
            ChooseWeapon(0);
        }

        ChooseWeapon(chosenWeaponNumber);
    }

    public void ChooseWeapon(int weaponNumber)
    {
        bigIcon.sprite = bigIcons[weaponNumber];
        title.text = titles[weaponNumber];
        description.text = descriptions[weaponNumber];
        if (audioPlayer != null)
        {
            audioPlayer.clip = click;
            audioPlayer.Play();
        }
        chosenWeaponNumber = weaponNumber;
        amtsText.text = "Amts: " + SaveScript.weaponAmts[weaponNumber];

        if (chosenWeaponNumber > 5)
        {
            combineButton.SetActive(true);
            combinePanel.SetActive(false);
        }
        if (chosenWeaponNumber < 6)
        {
            combinePanel.SetActive(false);
            combineButton.SetActive(false);
        }

        if(chosenWeaponNumber == 6)
        {
            useButton.SetActive(false);
        }
        else
        {
            useButton.SetActive(true);
        }

    }

    public void CombineAction()
    {
        combinePanel.SetActive(true);

        if(chosenWeaponNumber == 6)
        {
            combineItems[1].transform.gameObject.SetActive(false);
            if(SaveScript.itemsPickedUp[2] == true)
            {
                combineUseButton.SetActive(true);
            }
            if (SaveScript.itemsPickedUp[2] == false)
            {
                combineUseButton.SetActive(false);
            }
        }
        if (chosenWeaponNumber == 7)
        {
            combineItems[1].transform.gameObject.SetActive(true);
            if (SaveScript.itemsPickedUp[2] == true && SaveScript.itemsPickedUp[3] == true)
            {
                combineUseButton.SetActive(true);
            }
            if (SaveScript.itemsPickedUp[2] == false || SaveScript.itemsPickedUp[3] == false)
            {
                combineUseButton.SetActive(false);
            }
        }
    }

    public void CombineAssignWeapon()
    {
        if (chosenWeaponNumber == 6)
        {
            SaveScript.weaponID = chosenWeaponNumber;
            if (sprayPanel.GetComponent<SprayScript>().sprayAmount <= 0.0f)
            {
                sprayPanel.GetComponent<SprayScript>().sprayAmount = 1.0f;
            }
        }
        if (chosenWeaponNumber == 7)
        {
            SaveScript.weaponID = chosenWeaponNumber += 1;
        }
        audioPlayer.clip = select;
        audioPlayer.Play();
    }

    public void AssignWeapon()
    {
        SaveScript.weaponID = chosenWeaponNumber;
        audioPlayer.clip = select;
        audioPlayer.Play();
    }
}
