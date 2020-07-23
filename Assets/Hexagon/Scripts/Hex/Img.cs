using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using static UnityEngine.Debug;
using Random = UnityEngine.Random;

public class Img : CustomBehaviour
{
    public HexColor hexColor;

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        SetColor();
    }

    protected void SetColor()
    {
        var randomColorOrder = Constants.HexColorMap[GameManager.GridManager.startPoint + No()];
        hexColor = (HexColor) Enum.ToObject(typeof(HexColor), randomColorOrder);
        Image.color = GameManager.Constants.colors[(int) hexColor];
    }

    private int No()
    {
        var name = gameObject.name;
        name = name.Remove(0, 4);
        return Convert.ToInt16(name);
    }
}