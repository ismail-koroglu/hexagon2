using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Img : CustomBehaviour
{
    public HexColor hexColor;

    public void Constructor(HexColor _hexColor, Color _color)
    {
        hexColor = _hexColor;
        Image.color = _color;
    }
}