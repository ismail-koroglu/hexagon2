using System.Collections;
using System.Collections.Generic;
using Hexagon.Basics;
using UnityEngine;
using TMPro;

public class BombMono : MonoBehaviour, IHex
{
    private int countDownNo;
    public HexColor GetColor { get; set; }
    public int No { get; set; }
    public TextMeshProUGUI countDownTm;
    public int GetCountDownNo {
        get => countDownNo;
        set {
            countDownNo = value;
            countDownTm.text = countDownNo.ToString();
        }
    }
}
