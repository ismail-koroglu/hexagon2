using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Utilities
{
    public static void Open(this CanvasGroup canvas)
    {
        canvas.alpha = 1;
        canvas.blocksRaycasts = true;
        canvas.interactable = true;
    }

    public static void Close(this CanvasGroup canvas)
    {
        canvas.alpha = 0;
        canvas.blocksRaycasts = false;
        canvas.interactable = false;
    }

    public static T Random<T>(this IEnumerable<T> enumerable)
    {
        System.Random r = new System.Random();
        var list = enumerable as IList<T> ?? enumerable.ToList();
        return list.ElementAt(r.Next(0, list.Count()));
    }

    public static int[] SortInt(int[] input)
    {
        return input.OrderBy((a) => a).ToArray();
    }

    public static bool IsCoinToss()
    {
        return UnityEngine.Random.Range(0, 100) > 50;
    }

    public static bool IsEven(this int no)
    {
        return no % 2 == 0;
    }

    public static bool IsNeighbor(Slot s0, Slot s1)
    {
        var result = false;
        foreach (var neighbor in s0.Neighbors)
        {
            if (neighbor == s1.no) result = true;
        }

        return result;
    }

    public static bool IsContaining(this IEnumerable<Slot> list, Slot coming)
    {
        foreach (var item in list)
        {
            if (item == coming)
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsContaining<T>(this IEnumerable<T> list, T coming) where T : class
    {
        foreach (var item in list)
        {
            if (item == coming)
            {
                return true;
            }
        }

        return false;
    }
}