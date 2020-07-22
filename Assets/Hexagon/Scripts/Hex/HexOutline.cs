using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;

public class HexOutline : CustomBehaviour
{
    public Image Frame;

    private void SendFrameForward()
    {
        StartCoroutine(StartIe());

        IEnumerator StartIe()
        {
            yield return new WaitForSeconds(.1f);
            Frame.transform.SetAsLastSibling();
        }
    }

    private void ShowFrame()
    {
        Frame.gameObject.SetActive(true);
    }

    private void HideFrame()
    {
        Frame.gameObject.SetActive(false);
    }

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        GameManager.OnSetTriple += SendFrameForward;
        GameManager.OnStartFalling += HideFrame;
        GameManager.OnStopFalling += ShowFrame;
    }

    private void OnDestroy()
    {
        GameManager.OnSetTriple -= SendFrameForward;
        GameManager.OnStartFalling -= HideFrame;
        GameManager.OnStopFalling -= ShowFrame;
    }
}