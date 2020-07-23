using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Constants : CustomBehaviour
{
    #region Storage

    public readonly V2[] leftTopCorner = {new V2(0, 3), new V2(1, 3), new V2(2, 3), new V2(3, 3), new V2(4, 3), new V2(5, 3)};
    public readonly V2[] rightTopCorner = {new V2(0, 5), new V2(1, 5), new V2(2, 4), new V2(3, 4), new V2(4, 4), new V2(5, 5)};
    public readonly V2[] leftBottomCorner = {new V2(0, 1), new V2(1, 1), new V2(2, 2), new V2(3, 2), new V2(4, 2), new V2(5, 1)};
    public readonly V2[] rightBottomCorner = {new V2(0, 0), new V2(1, 0), new V2(2, 0), new V2(3, 0), new V2(4, 0), new V2(5, 0)};
    public readonly V2[] topEdgeOdd = {new V2(0, 5), new V2(1, 2), new V2(2, 2), new V2(3, 3), new V2(4, 4), new V2(5, 5)};
    public readonly V2[] topEdgeEven = {new V2(0, 4), new V2(1, 3), new V2(2, 3), new V2(3, 3), new V2(4, 4), new V2(5, 4)};
    public readonly V2[] bottomEdgeOdd = {new V2(0, 0), new V2(1, 1), new V2(2, 1), new V2(3, 1), new V2(4, 0), new V2(5, 0)};
    public readonly V2[] bottomEdgeEven = {new V2(0, 0), new V2(1, 1), new V2(2, 2), new V2(3, 2), new V2(4, 5), new V2(5, 5)};
    public readonly V2[] leftEdge = {new V2(0, 1), new V2(1, 1), new V2(2, 2), new V2(3, 3), new V2(4, 3), new V2(5, 1)};
    public readonly V2[] rightEdge = {new V2(0, 0), new V2(1, 0), new V2(2, 0), new V2(3, 4), new V2(4, 4), new V2(5, 5)};
    public readonly V2[] insideOdd = {new V2(0, 0), new V2(1, 1), new V2(2, 2), new V2(3, 3), new V2(4, 4), new V2(5, 5)};
    public readonly V2[] insideEven = {new V2(0, 0), new V2(1, 1), new V2(2, 2), new V2(3, 3), new V2(4, 4), new V2(5, 5)};

    public readonly V2[] sliceHexSelectOdd0 = {new V2(0, -1), new V2(-1, 0)};
    public readonly V2[] sliceHexSelectOdd1 = {new V2(0, -1), new V2(1, 0)};
    public readonly V2[] sliceHexSelectOdd2 = {new V2(1, 0), new V2(1, 1)};
    public readonly V2[] sliceHexSelectOdd3 = {new V2(1, 1), new V2(0, 1)};
    public readonly V2[] sliceHexSelectOdd4 = {new V2(-1, 1), new V2(0, 1)};
    public readonly V2[] sliceHexSelectOdd5 = {new V2(-1, 0), new V2(-1, 1)};

    public readonly V2[] sliceHexSelectEven0 = {new V2(0, -1), new V2(-1, -1)};
    public readonly V2[] sliceHexSelectEven1 = {new V2(0, -1), new V2(1, -1)};
    public readonly V2[] sliceHexSelectEven2 = {new V2(1, -1), new V2(1, 0)};
    public readonly V2[] sliceHexSelectEven3 = {new V2(1, 0), new V2(0, 1)};
    public readonly V2[] sliceHexSelectEven4 = {new V2(-1, 0), new V2(0, 1)};
    public readonly V2[] sliceHexSelectEven5 = {new V2(-1, -1), new V2(-1, 0)};

    private static readonly string[] LeftTopCornerNeighbor = {"0", "0", "1", "x", "0", "0"};
    private static readonly string[] RightTopCornerNeighbor = {"0", "0", "0", "x", "x-1", "-1"};
    private static readonly string[] LeftBottomCornerNeighbor = {"-x", "-x+1", "1", "0", "0", "0"};
    private static readonly string[] RightBottomCornerNeighbor = {"-x", "0", "0", "0", "0", "-1"};
    private static readonly string[] TopEdgeOddNeighbor = {"0", "1", "x+1", "x", "x-1", "-1"};
    private static readonly string[] TopEdgeEvenNeighbor = {"0", "0", "1", "x", "-1", "0"};
    private static readonly string[] BottomEdgeOddNeighbor = {"-x", "1", "0", "0", "0", "-1"};
    private static readonly string[] BottomEdgeEvenNeighbor = {"-x", "-x+1", "1", "0", "-1", "-x-1"};
    private static readonly string[] LeftEdgeNeighbor = {"-x", "-x+1", "1", "x", "0", "0"};
    private static readonly string[] RightEdgeNeighbor = {"-x", "0", "0", "x", "x-1", "-1"};
    private static readonly string[] InsideOddNeighbor = {"-x", "1", "x+1", "x", "x-1", "-1"};
    private static readonly string[] InsideEvenNeighbor = {"-x", "-x+1", "1", "x", "-1", "-x-1"};

    private static readonly Dictionary<HexPosType, string[]> HexNeighborDict = new Dictionary<HexPosType, string[]>
    {
        {HexPosType.LeftTopCorner, LeftTopCornerNeighbor},
        {HexPosType.RightTopCorner, RightTopCornerNeighbor},
        {HexPosType.LeftBottomCorner, LeftBottomCornerNeighbor},
        {HexPosType.RightBottomCorner, RightBottomCornerNeighbor},
        {HexPosType.TopEdgeOdd, TopEdgeOddNeighbor},
        {HexPosType.TopEdgeEven, TopEdgeEvenNeighbor},
        {HexPosType.BottomEdgeOdd, BottomEdgeOddNeighbor},
        {HexPosType.BottomEdgeEven, BottomEdgeEvenNeighbor},
        {HexPosType.LeftEdge, LeftEdgeNeighbor},
        {HexPosType.RightEdge, RightEdgeNeighbor},
        {HexPosType.InsideOdd, InsideOddNeighbor},
        {HexPosType.InsideEven, InsideEvenNeighbor},
    };

    public static readonly int[] HexColorMap =
    {
        0, 0, 2, 3, 4, 5, 0, 1, 2, 3,
        0, 1, 1, 3, 4, 5, 0, 1, 2, 3,
        0, 1, 2, 2, 4, 5, 0, 1, 2, 3,
        0, 1, 2, 3, 3, 5, 0, 1, 2, 3,
        0, 1, 2, 3, 4, 4, 0, 1, 2, 3,
        0, 1, 2, 3, 4, 5, 5, 1, 2, 3,
        0, 1, 2, 3, 4, 5, 0, 0, 2, 3,
        0, 1, 1, 3, 4, 5, 0, 1, 1, 3,
        0, 1, 2, 3, 3, 5, 0, 1, 2, 3,
        0, 1, 2, 3, 5, 5, 1, 1, 2, 3,
    };

    public readonly Color[] colors = new Color[6]
    {
        new Color(0.19f, 0.58f, 0.83f),
        new Color(0.16f, 0.78f, 0.41f),
        new Color(0.90f, 0.47f, 0.13f),
        new Color(0.58f, 0.33f, 0.70f),
        new Color(1.00f, 0.18f, 0.22f),
        new Color(0.93f, 0.74f, 0.03f)
    };

    #endregion

    /****************************************************************************************/

    public readonly int OneSlotPoint = 5;
    public readonly int CountDownStartValue = 3;
    public readonly int[] tripleKeyEven = {0, 1, 1, 1, 0, 2};
    public readonly int[] tripleKeyOdd = {1, 2, 0, 2, 1, 1};

    /****************************************************************************************/
    public static HexPosType GetHexPosType(int x, int y, V2 gridSize)
    {
        if (x == 0 && y == 0) //CORNER
        {
            return HexPosType.LeftTopCorner;
        }

        if (x == gridSize.x - 1 && y == 0)
        {
            return HexPosType.RightTopCorner;
        }

        if (x == 0 && y == gridSize.y - 1)
        {
            return HexPosType.LeftBottomCorner;
        }

        if (x == gridSize.x - 1 && y == gridSize.y - 1)
        {
            return HexPosType.RightBottomCorner;
        }

        if (y == 0 && (x != 0 || x != gridSize.x - 1) && x % 2 == 0) //EDGES
        {
            return HexPosType.TopEdgeEven;
        }

        if (y == 0 && (x != 0 || x != gridSize.x - 1) && x % 2 == 1) //EDGES
        {
            return HexPosType.TopEdgeOdd;
        }

        if (x == 0 && (y != 0 || y != gridSize.y - 1))
        {
            return HexPosType.LeftEdge;
        }

        if (x == gridSize.x - 1 && (y != 0 || y != gridSize.y - 1))
        {
            return HexPosType.RightEdge;
        }

        if (y == gridSize.y - 1 && (x != 0 || x != gridSize.x - 1) && x % 2 == 0)
        {
            return HexPosType.BottomEdgeEven;
        }

        if (y == gridSize.y - 1 && (x != 0 || x != gridSize.x - 1) && x % 2 == 1)
        {
            return HexPosType.BottomEdgeOdd;
        }

        if (x % 2 == 0)
        {
            return HexPosType.InsideEven;
        }

        {
            return HexPosType.InsideOdd;
        }
    }

    public int[] GetNeighbor(HexPosType hexPosType)
    {
        var selectedNeighbor = HexNeighborDict[hexPosType];
        var neighborArr = new[] {0, 0, 0, 0, 0, 0};
        for (var i = 0; i < 6; i++)
        {
            neighborArr[i] = NeighborMatch(selectedNeighbor[i]);
        }

        return neighborArr;
    }

    private int NeighborMatch(string str)
    {
        var x = GameManager.GridManager.GetGridSize.x;
        switch (str)
        {
            case "0":
                return 0;
            case "1":
                return 1;
            case "-1":
                return -1;
            case "x":
                return x;
            case "-x":
                return -x;
            case "x-1":
                return x - 1;
            case "x+1":
                return x + 1;
            case "-x+1":
                return -x + 1;
            case "-x-1":
                return -x - 1;
            default:
                return 0;
        }
    }
}

public enum HexPosType
{
    LeftTopCorner = 0,
    RightTopCorner = 1,
    LeftBottomCorner = 2,
    RightBottomCorner = 3,
    TopEdgeOdd = 4,
    TopEdgeEven = 5,
    BottomEdgeOdd = 6,
    BottomEdgeEven = 7,
    LeftEdge = 8,
    RightEdge = 9,
    InsideOdd = 10,
    InsideEven = 11
}

public enum Panels
{
    Hud = 0,
}

public enum HexColor
{
    Blue = 0,
    Green = 1,
    Orange = 2,
    Purple = 3,
    Red = 4,
    Yellow = 5
}

public enum GridSize
{
    EightNine,
    EightSix,
    SixNine,
    SixSix
}

public enum RotateType
{
    Cw = -1,
    Ccw = 1
}

public enum ColorCount
{
    Five = 5,
    Six = 6
}