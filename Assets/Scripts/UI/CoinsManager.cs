using System;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public TextMeshProUGUI CoinsText;
    public int CoinsCount;


    private void Start()
    {
        CoinsCount = 100;
        SetCoinsText();
    }


    public void SpendCoins(int amount)
    {
        CoinsCount = CoinsCount - amount;
        SetCoinsText();

    }


    public void AddCoins(int amount)
    {
        CoinsCount = CoinsCount + amount;
        SetCoinsText();
    }

    private void SetCoinsText()
    {
        CoinsText.text = CoinsCount.ToString();
    }
    
}
