using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Debug;

public class Calculator : CustomBehaviour
{
    private GridManager gridManager;

    public bool IsMatching()
    {
        return false;
    }

    public List<Slot> neighborSlots = new List<Slot>();
    public List<Slot> sameNeighbor = new List<Slot>();
    public List<Relation> relations = new List<Relation>();
    public List<Slot> OnePlusSlots = new List<Slot>();
    private HexColor keyColor;

    private void Calculate()
    {
        var sameColorCounter = 0;
        var neighborCounter = 0;
        var key = gridManager.KeyNo;
        var neighbors = gridManager.AllSlots[key].Neighbors;
        keyColor = gridManager.AllSlots[key].img.hexColor;
        var neighborCount = 0;
        sameNeighbor.Clear();
        neighborSlots.Clear();
        relations.Clear();
        OnePlusSlots.Clear();

        neighborSlots.AddRange(from no in neighbors where no != -1 select gridManager.AllSlots[no]);
        if (neighborSlots.Count == 6)
        {
            foreach (var neighborSlot in neighborSlots)
            {
                OnePlusSlots.Add(neighborSlot);
            }

            OnePlusSlots.Add(neighborSlots[0]);


            for (var i = 0; i < OnePlusSlots.Count; i++)
            {
                if (i < OnePlusSlots.Count - 1)
                {
                    var s0 = OnePlusSlots[i];
                    var s1 = OnePlusSlots[i + 1];
                    CreateRelation(OnePlusSlots[i], OnePlusSlots[i + 1]);
                }
            }
        }

        Log("___ :" + relations.Count);
    }


    private void CreateRelation(Slot s0, Slot s1)
    {
        if (Utilities.IsNeighbor(s0, s1) && s0.img.hexColor == keyColor && s1.img.hexColor == keyColor)
        {
            var relation = new Relation(s0, s1, keyColor);
            if (!relations.IsContaining(relation))
            {
                relations.Add(relation);
            }
        }
    }

    /****************************************************************************************/

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        gridManager = GameManager.GridManager;
        // GameManager.OnCheckMatching += CheckMatching;
        GameManager.OnCalculate += Calculate;
    }

    private void OnDestroy()
    {
        // GameManager.OnCheckMatching -= CheckMatching;
        GameManager.OnCalculate -= Calculate;
    }
}