using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class Calculator : CustomBehaviour
{
    public bool IsMatching()
    {
        var EnviroHex = GameManager.GridManager.EnviroHex;
        var TripleHex = GameManager.GridManager.TripleHex;
        foreach (var i in EnviroHex)
        {
            foreach (var j in EnviroHex)
            {
                foreach (var k in TripleHex)
                {
                    if (i != j && i.HexColor == j.HexColor)
                    {
                        if (Vector3.Distance(i.transform.position, j.transform.position) < 140)
                        {
                            Log("___ :" + i.No);
                            Log("___ :" + j.No);
                        }
                    }
                }
            }
        }

        return false;
    }

    /****************************************************************************************/

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        // GameManager.OnCheckMatching += CheckMatching;
    }

    private void OnDestroy()
    {
        // GameManager.OnCheckMatching -= CheckMatching;
    }
}