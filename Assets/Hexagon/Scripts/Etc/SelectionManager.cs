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
    public int KeyNo;
    private Slice slice;

    private void SetOutline()
    {
        slice = GameManager.InputManager.SelectedSlice;
        var tripleOneNo = slice.transform.parent.parent.GetComponent<HexMono>().No;
        var sliceHexSelect = (tripleOneNo % 2 == 1) ? gridManager.SliceHexMapOdd : gridManager.SliceHexMapEven;
        var twoCoords = sliceHexSelect[slice.no];
        var tripleTwoNo = tripleOneNo + gridManager.GetGridSize.x * twoCoords[0].y + twoCoords[0].x;
        var tripleThreeNo = tripleOneNo + gridManager.GetGridSize.x * twoCoords[1].y + twoCoords[1].x;
        var hexMonoNoMod = slice.transform.parent.parent.GetComponent<HexMono>().No % 2;
        var tripleKeys = hexMonoNoMod == 0 ? GameManager.Constants.tripleKeyEven : GameManager.Constants.tripleKeyOdd;
        var keyNo = tripleKeys[slice.no];
        gridManager.TripleNos = Utilities.SortInt(new[] {tripleOneNo, tripleTwoNo, tripleThreeNo});
        KeyNo = gridManager.AllHex[gridManager.TripleNos[keyNo]].No;
        SetEnviro();
    }

    private void SetEnviro()
    {
        gridManager.Enviro.Clear();
        gridManager.EnviroHex.Clear();
        gridManager.TripleHex.Clear();
        for (var i = 0; i < 3; i++)
        {
            var hexNeighbor = gridManager.AllHex[gridManager.TripleNos[i]].Neighbors;
            foreach (var item in hexNeighbor)
            {
                if (!IsExist(item) && !IsTriple(item) && item != -1)
                {
                    gridManager.Enviro.Add(item);
                }
            }
        }

        gridManager.Enviro.Sort();
        foreach (var item in gridManager.Enviro)
        {
            gridManager.EnviroHex.Add(GameManager.GridManager.AllHex[item]);
        }

        foreach (var item in gridManager.TripleNos)
        {
            gridManager.TripleHex.Add(GameManager.GridManager.AllHex[item]);
        }

        // SetColor();
    }

    private bool IsExist(int coming)
    {
        return gridManager.Enviro.Any(item => coming == item);
    }

    private bool IsTriple(int coming)
    {
        return gridManager.TripleNos.Any(item => coming == item);
    }

    private void SetColor()
    {
        foreach (var item in gridManager.AllHex)
        {
            item.Image.color = Color.gray;
        }

        foreach (var item in gridManager.Enviro)
        {
            gridManager.AllHex[item].Image.color = Color.green;
        }
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        GameManager.OnSelectHex += SetOutline;
        gridManager = GameManager.GridManager;
    }

    private void OnDestroy()
    {
        GameManager.OnSelectHex -= SetOutline;
    }
}