using System;
using System.Collections.Generic;
using System.Linq;
using Hexagon.Basics;
using UnityEngine;
using static UnityEngine.Debug;

public class SelectionManager : CustomBehaviour
{
    /****************************************************************************************/
    private GridManager gridManager;

    // public int KeyNo;
    private Slice slice;

    private void SetOutline()
    {
        slice = GameManager.InputManager.SelectedSlice;
        var tripleOneNo = slice.transform.parent.parent.GetComponent<Slot>().no;
        var sliceHexSelect = (tripleOneNo % 2 == 1) ? gridManager.SliceHexMapOdd : gridManager.SliceHexMapEven;
        var twoCoords = sliceHexSelect[slice.no];
        var tripleTwoNo = tripleOneNo + gridManager.GetGridSize.x * twoCoords[0].y + twoCoords[0].x;
        var tripleThreeNo = tripleOneNo + gridManager.GetGridSize.x * twoCoords[1].y + twoCoords[1].x;
        var hexMonoNoMod = slice.transform.parent.parent.GetComponent<Slot>().no % 2;
        var tripleKeys = hexMonoNoMod == 0 ? GameManager.Constants.tripleKeyEven : GameManager.Constants.tripleKeyOdd;
        var keyNo = tripleKeys[slice.no];

        gridManager.TripleNos.Clear();
        gridManager.TripleNos.Add(tripleOneNo);
        gridManager.TripleNos.Add(tripleTwoNo);
        gridManager.TripleNos.Add(tripleThreeNo);
        gridManager.TripleNos.Sort();
        gridManager.KeyNo = gridManager.AllSlots[gridManager.TripleNos[keyNo]].no;
        gridManager.KeySlot = gridManager.AllSlots[keyNo];
        SetEnviro();
    }

    private void SetEnviro()
    {
        gridManager.EnviroNos.Clear();
        gridManager.EnviroSlots.Clear();
        gridManager.TripleSlots.Clear();
        gridManager.TripleImgs.Clear();
        gridManager.BigTriangleSlots.Clear();
        for (var i = 0; i < 3; i++)
        {
            var hexNeighbor = gridManager.AllSlots[gridManager.TripleNos[i]].Neighbors;
            foreach (var item in hexNeighbor)
            {
                if (!IsExist(item) && !IsTriple(item) && item != -1)
                {
                    gridManager.EnviroNos.Add(item);
                }
            }
        }
        gridManager.EnviroNos.Sort();
        foreach (var no in gridManager.EnviroNos)
        {
            var slot = GameManager.GridManager.AllSlots[no];
            gridManager.EnviroSlots.Add(slot);
            gridManager.BigTriangleSlots.Add(slot);
        }

        foreach (var no in gridManager.TripleNos)
        {
            gridManager.TripleImgs.Add(gridManager.AllSlots[no].img);
            var slot = gridManager.AllSlots[no];
            gridManager.TripleSlots.Add(slot);
            gridManager.BigTriangleSlots.Add(slot);
        }
    }

    private bool IsExist(int coming)
    {
        return gridManager.EnviroNos.Any(item => coming == item);
    }

    private bool IsTriple(int coming)
    {
        return gridManager.TripleNos.Any(item => coming == item);
    }

    private void SetColor()
    {
        foreach (var item in gridManager.AllSlots)
        {
            item.Image.color = Color.gray;
        }

        foreach (var item in gridManager.EnviroNos)
        {
            gridManager.AllSlots[item].Image.color = Color.green;
        }
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        GameManager.OnSetTriple += SetOutline;
        gridManager = GameManager.GridManager;
    }

    private void OnDestroy()
    {
        GameManager.OnSetTriple -= SetOutline;
    }
}