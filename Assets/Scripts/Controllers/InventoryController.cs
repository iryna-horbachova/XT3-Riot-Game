using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{

    public GameObject inventoryPanelUI;

    private GameObject player;

    private string boosterCountTextString = "You have: ";

    // Count of items in stock
    private int mayonnaiseCount; // 0
    private int samogonCount; // 1
    private int speedPillCount; // 2
    private int[] counts;

    // Strings to use in PlayerPrefs
    private string mayonnaiseCountString = "MayonnaiseCount";
    private string samogonCountString = "CardsCount";
    private string speedPillCountString = "SpeedPillCount";
    private string[] countStrings;

    // Tracking whether the item was already used in a level
    private bool mayonnaiseUsed = false;
    private bool samogonUsed = false;
    private bool speedPillUsed = false;
    private bool[] usedBoosters;

    // Text indicating how many items user has in stocl
    [SerializeField] private Text mayonnaiseCountText;
    [SerializeField] private Text samogonCountText;
    [SerializeField] private Text speedPillCountText;
    private Text[] boostersTexts;

    // Buttons to use item
    [SerializeField] private Button useMayonnaiseButton;
    [SerializeField] private Button useSamogonButton;
    [SerializeField] private Button useSpeedPillButton;
    private Button[] boostersButtons;

    void Start()
    {
        player = GameObject.Find("OrangeRobot");

        mayonnaiseCount = PlayerPrefs.GetInt(mayonnaiseCountString, 0);
        samogonCount = PlayerPrefs.GetInt(samogonCountString, 0);
        speedPillCount = PlayerPrefs.GetInt(speedPillCountString, 0);

        usedBoosters = new bool[] { mayonnaiseUsed, samogonUsed, speedPillUsed };
        counts = new int[] { mayonnaiseCount, samogonCount, speedPillCount };
        countStrings = new string[] { mayonnaiseCountString, samogonCountString, speedPillCountString };
        boostersTexts = new Text[] { mayonnaiseCountText, samogonCountText, speedPillCountText };
        boostersButtons = new Button[] { useMayonnaiseButton, useSamogonButton, useSpeedPillButton };

        UpdateUI();
    }

    public void ShowInventoryPanel()
    {
        inventoryPanelUI.SetActive(true);
    }

    public void QuitPanel()
    {
        inventoryPanelUI.SetActive(false);
    }

    public void UseBooster(int productOption)
    {
        if (usedBoosters[productOption] || counts[productOption] < 1) {
            return;
        }

        usedBoosters[productOption] = true;
        counts[productOption]--;
        PlayerPrefs.SetInt(countStrings[productOption], counts[productOption]);
        ApplyBooster(productOption);
        UpdateUI();
    }

    private void ApplyBooster(int productOption)
    {
        switch(productOption)
        {
            // Mayonnaise
            case 0:
                PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
                playerAttack.IncreaseAttackDamage(5);
                break;
            // Samogon
            case 1:
                PlayerLife playerLife = player.GetComponent<PlayerLife>();
                playerLife.IncreaseHealth(20);
                break;
            // Speed Pill
            case 2:
                PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
                playerMovement.IncreaseMovementSpeed(1.5f);
                break;
            default:
                break;
        }
    }

    private void UpdateUI()
    {

        for(int i = 0; i < counts.Length; i++)
        {
            SetCountText(boosterCountTextString, counts[i], boostersTexts[i]);
            CheckAbilityToUse(i);
        }
    }

    private void CheckAbilityToUse(int productOption)
    {
        if (usedBoosters[productOption] || counts[productOption] < 1)
        {
            boostersButtons[productOption].interactable = false;
        }
    }


    private void SetCountText(string text, int count, Text textfield)
    {
        textfield.text = text + count;
    }
}
