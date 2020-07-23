using System;
using System.Collections;
using System.Collections.Generic;
using Hexagon.Basics;
using UnityEngine;
using TMPro;
using DG.Tweening;
using static UnityEngine.Debug;

public class Bomb : Img
{
    private int countDown;
    public TextMeshProUGUI countDownTm;
    private bool isFalling;

    private void CountDown()
    {
        countDown--;
        countDownTm.text = countDown.ToString();
        isFalling = true;
        if (countDown <= 0)
        {
            transform.DOShakePosition(3, 20, 90, 50f, true).OnComplete(() =>
            {
                GameManager.FinishGame();
                gameObject.SetActive(false);
            });
        }

        var pos = transform.position;
        transform.DOShakePosition(2, 20, 90, 50f, true).OnComplete(() => transform.position = pos);
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