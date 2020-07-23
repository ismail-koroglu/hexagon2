using System.Collections;
using System.Collections.Generic;
using Hexagon.Basics;
using UnityEngine;
using TMPro;
using static UnityEngine.Debug;

public class Bomb : Img
{
    private int countDown;
    public TextMeshProUGUI countDownTm;
    private bool isFalling;

    private void CountDown()
    {
        // if (!isFalling) countDown--;
        countDown--;
        countDownTm.text = countDown.ToString();
        isFalling = true;
        if (countDown <= 0) GameManager.FinishGame();
        Log("___ :" + countDown);
    }

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        countDown = GameManager.Constants.CountDownStartValue;
        countDownTm.text = countDown.ToString();
        GameManager.OnStopFalling += CountDown;
        SetColor();
    }

    private void OnDestroy()
    {
        GameManager.OnStopFalling -= CountDown;
    }
}