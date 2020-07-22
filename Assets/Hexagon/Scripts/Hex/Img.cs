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

    private void SetColor()
    {
        var randomColorOrder = Constants.HexColorMap[GameManager.GridManager.startPoint + No()];
        // var randomColorOrder = Random.Range(0, (int) GameManager.GridManager.colorCountEnum);
        hexColor = (HexColor) Enum.ToObject(typeof(HexColor), randomColorOrder);
        Image.color = GameManager.Constants.colors[(int) hexColor];
        Log("___ :" + No());
    }

    private int No()
    {
        var c = 0;
        foreach (var img in GameManager.GridManager.AllImgs)
        {
            c++;
            if (this == img) return c;
        }

        return 0;
    }
}