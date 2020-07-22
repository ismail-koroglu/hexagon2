public struct V2
{
    public int x, y;

    public static V2 zero => new V2(0, 0);

    public static V2 one => new V2(1, 1);

    public static V2 up => new V2(0, 1);

    public static V2 right => new V2(1, 0);

    public V2(int a, int b)
    {
        x = a;
        y = b;
    }

    public static V2 operator +(V2 a, V2 b)
    {
        return new V2(a.x + b.x, a.y + b.y);
    }

    public static V2 operator -(V2 a, V2 b)
    {
        return new V2(a.x - b.x, a.y - b.y);
    }

    public static V2 operator *(V2 a, V2 b)
    {
        return new V2(a.x * b.x, a.y * b.y);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ")";
    }
}

public struct V3
{
    public int a, b, c;

    public V3(int _a, int _b, int _c)
    {
        a = _a;
        b = _b;
        c = _c;
    }
}

public class Relation
{
    public Slot slot0;
    public Slot slot1;
    private HexColor hexColor;

    public Relation(Slot _s0, Slot _s1, HexColor _hexColor)
    {
        slot0 = _s0;
        slot1 = _s1;
        hexColor = _hexColor;
    }
}