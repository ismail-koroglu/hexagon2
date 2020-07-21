﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using static UnityEngine.Debug;

public class GridManager : CustomBehaviour
{
    public List<Slot> AllSlots = new List<Slot>();
    public List<Img> AllImgs = new List<Img>();
    public List<int> Enviro = new List<int>();
    public int[] TripleNos = new int[3];
    public List<Slot> EnviroHex = new List<Slot>();

    public List<Slot> TripleHex = new List<Slot>();

    /****************************************************************************************/

    public GridSize gridSizeEnum;
    public ColorCount colorCountEnum;
    public Transform slotBkg, imgBkg;
    public V2 GetGridSize => gridSize;
    public GameObject slot, bomb, img;
    public List<V2[]> SliceMap;
    public List<V2[]> SliceHexMapOdd;
    public List<V2[]> SliceHexMapEven;
    private V2 firstHexPos;
    private readonly Vector2 hexDiff = new Vector2(120, 69); //Difference between two hex;
    private Constants Constants;
    private V2 gridSize;

    /****************************************************************************************/
    private void Start()
    {
        SliceMap = new List<V2[]>
        {
            Constants.leftTopCorner,
            Constants.rightTopCorner,
            Constants.leftBottomCorner,
            Constants.rightBottomCorner,
            Constants.topEdgeOdd,
            Constants.topEdgeEven,
            Constants.bottomEdgeOdd,
            Constants.bottomEdgeEven,
            Constants.leftEdge,
            Constants.rightEdge,
            Constants.insideOdd,
            Constants.insideEven,
        };
        SliceHexMapOdd = new List<V2[]>
        {
            Constants.sliceHexSelectOdd0,
            Constants.sliceHexSelectOdd1,
            Constants.sliceHexSelectOdd2,
            Constants.sliceHexSelectOdd3,
            Constants.sliceHexSelectOdd4,
            Constants.sliceHexSelectOdd5
        };
        SliceHexMapEven = new List<V2[]>
        {
            Constants.sliceHexSelectEven0,
            Constants.sliceHexSelectEven1,
            Constants.sliceHexSelectEven2,
            Constants.sliceHexSelectEven3,
            Constants.sliceHexSelectEven4,
            Constants.sliceHexSelectEven5
        };
        // Triangle = new Triangle();

        // Slots = new List<Slot>();
        // TripleHex =
        SetGridSize();
        GenerateHex();
    }

    private void SetGridSize()
    {
        switch (gridSizeEnum)
        {
            case GridSize.EightNine:
                gridSize = new V2(8, 9);
                firstHexPos = new V2(-420, 500);
                break;
            case GridSize.EightSix:
                gridSize = new V2(8, 6);
                firstHexPos = new V2(-420, 350);
                break;
            case GridSize.SixNine:
                gridSize = new V2(6, 9);
                firstHexPos = new V2(-300, 500);
                break;
            case GridSize.SixSix:
                gridSize = new V2(6, 6);
                firstHexPos = new V2(-300, 300);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void GenerateHex()
    {
        for (var y = 0; y < gridSize.y; y++)
        {
            for (var x = 0; x < gridSize.x; x++)
            {
                var order = y * ((int) gridSize.x) + x;
                var lowHighMod = x % 2;
                var posX = firstHexPos.x + (hexDiff.x * x);
                var posY = lowHighMod == 0 ? 0 : -hexDiff.y;
                posY -= (y * hexDiff.y * 2) - firstHexPos.y;


                var slotGo = Instantiate(slot);
                slotGo.transform.position = new Vector3(posX, posY, 0);
                slotGo.transform.parent = slotBkg;
                slotGo.name = "Hex_" + order.ToString("00");
                var slotCmp = slotGo.GetComponent<Slot>();
                slotCmp.Initialize(GameManager);

                var imgGo = Instantiate(img);
                imgGo.transform.position = new Vector3(posX, posY, 0);
                imgGo.transform.parent = imgBkg;
                imgGo.name = "Hex_" + order.ToString("00");
                var imgCmp = imgGo.GetComponent<Img>();
                imgCmp.Initialize(GameManager);
                AllImgs.Add(imgCmp);

                AllSlots.Add(slotCmp);

                SetHexConstructor(slotCmp, imgCmp, order);
            }
        }
    }

    private void SetHexConstructor(Slot slot, Img img, int no)
    {
        var randomColorOrder = Random.Range(0, (int) colorCountEnum);
        var color = Constants.colors[randomColorOrder];
        var colorEnum = (HexColor) Enum.ToObject(typeof(HexColor), randomColorOrder);
        var isStar = Random.Range(0, 100) > 90;
        slot.Constructor(no);
        img.Constructor(colorEnum, color);
    }

    /****************************************************************************************/

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        Constants = GameManager.Constants;
    }
}