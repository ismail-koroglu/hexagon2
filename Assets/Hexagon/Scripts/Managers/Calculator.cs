using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Debug;

public class Calculator : CustomBehaviour
{
    public bool IsMatching()
    {
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