using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;
using static UnityEngine.Debug;

public class ImgManager : CustomBehaviour
{
    private GridManager gridManager;

    private void SetTripleImgsToSlots()
    {
        // foreach (var slot in gridManager.TripleSlots)
        // {
        //     slot.img = null;
        // }

        foreach (var slot in gridManager.TripleSlots)
        {
            foreach (var img in gridManager.TripleImgs)
            {
                if (IsClose(slot, img))
                {
                    slot.img = img;
                }
            }
        }

        gridManager.TripleImgs.Clear();
        foreach (var slot in gridManager.TripleSlots)
        {
            gridManager.TripleImgs.Add(slot.img);
        }
    }

    private static bool IsClose(Component c0, Component c1)
    {
        return Vector3.Distance(c0.transform.position, c1.transform.position) < 10;
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        gridManager = GameManager.GridManager;
        GameManager.OnStopRotation += SetTripleImgsToSlots;
    }

    private void OnDestroy()
    {
        GameManager.OnStopRotation -= SetTripleImgsToSlots;
    }
}