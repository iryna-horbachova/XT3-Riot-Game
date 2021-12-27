using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : MonoBehaviour
{
    
    private int cardsCount; 
    private int mayonnaiseCount; // 0
    private int samogonCount; // 1
    private int speedPillCount; // 2
    private int[] counts;

    private string cardsCountString = "Cards";
    private string mayonnaiseCountString = "MayonnaiseCount";
    private string samogonCountString = "CardsCount";
    private string speedPillCountString = "SpeedPillCount";
    private string[] countStrings;

    private int mayonnaisePrice = 30;
    private int samogonPrice = 40;
    private int speedPillPrice = 50;
    private int[] prices;

    private string boosterCountTextString = "You have: ";
    private string cardsCountTextString = "Your cards: ";

    [SerializeField] private Text cardsCountText;
    [SerializeField] private Text mayonnaiseCountText;
    [SerializeField] private Text samogonCountText;
    [SerializeField] private Text speedPillCountText;
    private Text[] boostersTexts;

    [SerializeField] private Button purchaseMayonnaiseButton;
    [SerializeField] private Button purchaseSamogonButton;
    [SerializeField] private Button purchaseSpeedPillButton;
    private Button[] boostersButtons;

    void Start()
    {
        cardsCount = PlayerPrefs.GetInt(cardsCountString, 0);
        mayonnaiseCount = PlayerPrefs.GetInt(mayonnaiseCountString, 0);
        samogonCount = PlayerPrefs.GetInt(samogonCountString, 0);
        speedPillCount = PlayerPrefs.GetInt(speedPillCountString, 0);

        prices = new int[] { mayonnaisePrice, samogonPrice, speedPillPrice };
        counts = new int[] { mayonnaiseCount, samogonCount, speedPillCount };
        countStrings = new string[] { mayonnaiseCountString, samogonCountString, speedPillCountString };
        boostersTexts = new Text[] { mayonnaiseCountText, samogonCountText, speedPillCountText };
        boostersButtons = new Button[] { purchaseMayonnaiseButton, purchaseSamogonButton, purchaseSpeedPillButton };
        UpdateUI();
    }

    public void Purchase(int productOption)
    {
        if (prices[productOption] > cardsCount) {
            return;
        }

        cardsCount -= prices[productOption];
        counts[productOption]++;
        PlayerPrefs.SetInt(cardsCountString, cardsCount);
        PlayerPrefs.SetInt(countStrings[productOption], counts[productOption]);
        UpdateUI();
    }

    private void UpdateUI()
    {
        SetCountText(cardsCountTextString, cardsCount, cardsCountText);

        for(int i = 0; i < prices.Length; i++)
        {
            SetCountText(boosterCountTextString, counts[i], boostersTexts[i]);
            CheckAbilityToPay(i);
        }
    }

    private void CheckAbilityToPay(int productOption)
    {
        if (prices[productOption] > cardsCount)
        {
            boostersButtons[productOption].interactable = false;
        }
    }


    private void SetCountText(string text, int count, Text textfield)
    {
        textfield.text = text + count;
    }
}
