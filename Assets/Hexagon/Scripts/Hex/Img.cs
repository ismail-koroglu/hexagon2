using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Img : CustomBehaviour
{
    public HexColor hexColor;

    public void Constructor(HexColor _hexColor, GameManager gameManager)
    {
        Initialize(gameManager);
        hexColor = _hexColor;
        Image.color = gameManager.Constants.colors[(int) _hexColor];
    }
}