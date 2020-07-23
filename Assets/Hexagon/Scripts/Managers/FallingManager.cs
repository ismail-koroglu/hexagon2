using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.Debug;


public class FallingManager : CustomBehaviour
{
    private GridManager gridManager;
    private List<Slot> emptySlots = new List<Slot>();
    private List<Slot> allSlots;
    private bool IsLooping = false;

    private void StartFalling()
    {
        IsLooping = true;
        StartCoroutine(StartIe());
        StartCoroutine(StopIe());
    }

    private void StopFalling()
    {
        IsLooping = false;
    }

    IEnumerator StartIe()
    {
        while (IsLooping)
        {
            yield return new WaitForSeconds(.1f);
            SlideUpSlots();
        }
    }

    IEnumerator StopIe()
    {
        yield return new WaitForSeconds(1);
        GameManager.StopFalling();
    }

    private void SlideUpSlots()
    {
        foreach (var slot in allSlots)
        {
            var emptyNo = slot.Neighbors[3];

            if (emptyNo != -1 && allSlots[emptyNo].img == null && slot.img != null)
            {
                slot.img.transform.DOMove(allSlots[emptyNo].transform.position, .5f).SetEase(Ease.InSine);
                allSlots[emptyNo].img = slot.img;
                slot.img = null;
            }
        }
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        gridManager = GameManager.GridManager;
        allSlots = gridManager.AllSlots;
        GameManager.OnStartFalling += StartFalling;
        GameManager.OnStopFalling += StopFalling;
    }

    private void OnDestroy()
    {
        GameManager.OnStartFalling -= StartFalling;
        GameManager.OnStopFalling -= StopFalling;
    }
}