using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;

public class HexOutline : CustomBehaviour
{
    public Image Frame;

    private void ShowFrame()
    {
        StartCoroutine(StartIe());

        IEnumerator StartIe()
        {
            yield return new WaitForSeconds(.1f);
            Frame.transform.SetAsLastSibling();
        }
    }

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        GameManager.OnSelectHex += ShowFrame;
    }

    private void OnDestroy()
    {
        GameManager.OnSelectHex -= ShowFrame;
    }
}