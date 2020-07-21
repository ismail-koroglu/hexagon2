using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Debug;
using TMPro;
using Hexagon.Basics;

public class Slot : CustomBehaviour
{
    public GameObject star;
    public int No;
    public HexColor HexColor;
    public HexPosType hexPosType;
    public TextMeshProUGUI tm;
    public Slice[] slices = new Slice[6];

    private bool IsStar;
    private V2[] selectedMap;
    private V2 gridSize;
    public int[] Neighbors = new int[6];
    private Dictionary<Slice, Slice> realSliceDict = new Dictionary<Slice, Slice>();

    public void Constructor(int no, bool isStar, Color color, HexColor hexColor)
    {
        No = no;
        IsStar = isStar;
        star.SetActive(IsStar);
        HexColor = hexColor;
        // GetComponent<Image>().color = color;
        Image.color = color;
        gridSize = GameManager.GridManager.GetGridSize;
        tm.text = no.ToString();
        SetHexPosType();
    }

    private void SetHexPosType()
    {
        hexPosType = Constants.GetHexPosType(No % gridSize.x, No / gridSize.x, gridSize);
        selectedMap = GameManager.GridManager.SliceMap[(int) hexPosType];
        SetNeighbors();
    }

    private void SetNeighbors()
    {
        var getNeighbor = GameManager.Constants.GetNeighbor(hexPosType);
        for (var i = 0; i < 6; i++)
        {
            var value = getNeighbor[i];
            if (value == 0) Neighbors[i] = -1;
            else
            {
                Neighbors[i] = value + No;
            }
        }
    }

    public Slice GetMappedSlice(int sliceNo)
    {
        for (var i = 0; i < selectedMap.Length; i++)
        {
            if (sliceNo == selectedMap[i].x)
            {
                return slices[selectedMap[i].y];
            }
        }

        return null;
    }
}