using TMPro;

public class Slot : CustomBehaviour
{
    public Img img;
    public int no;
    public HexPosType hexPosType;
    public TextMeshProUGUI tm;
    public Slice[] slices = new Slice[6];
    public int[] Neighbors = new int[6];
    private V2[] selectedMap;
    private V2 gridSize;

    public void Constructor(int _no, Img _img)
    {
        no = _no;
        gridSize = GameManager.GridManager.GetGridSize;
        tm.text = _no.ToString();
        img = _img;
        SetHexPosType();
    }

    private void SetHexPosType()
    {
        hexPosType = Constants.GetHexPosType(no % gridSize.x, no / gridSize.x, gridSize);
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
                Neighbors[i] = value + no;
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