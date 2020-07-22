using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Debug;

public class Calculator : CustomBehaviour
{
    private GridManager gridManager;
    private bool result;

    public List<Slot> neighborSlots = new List<Slot>();
    public List<Slot> sameNeighbor = new List<Slot>();
    public List<Relation> relations = new List<Relation>();
    public List<Slot> OnePlusSlots = new List<Slot>();
    public List<Slot> RelatedSlots = new List<Slot>();
    private HexColor keyColor;
    private int centerNo = 0;

    public bool IsMatching()
    {
        GameManager.ImgManager.SetTripleImgsToSlots();
        CalculateForTriple();
        return result;
    }

    private void CalculateForTriple()
    {
        result = false;
        foreach (var no in gridManager.TripleNos)
        {
            CalculateMain(no);
        }
    }

    private void CalculateMain(int _centerNo)
    {
        var sameColorCounter = 0;
        var neighborCounter = 0;
        centerNo = _centerNo;
        var neighbors = gridManager.AllSlots[centerNo].Neighbors;
        keyColor = gridManager.AllSlots[centerNo].img.hexColor;
        var neighborCount = 0;
        sameNeighbor.Clear();
        neighborSlots.Clear();
        relations.Clear();
        OnePlusSlots.Clear();
        RelatedSlots.Clear();
        neighborSlots.AddRange(from no in neighbors where no != -1 select gridManager.AllSlots[no]);

        if (neighborSlots.Count == 6)
        {
            FindRelationsForSix();
        }
        else
        {
            FindRelationForLess();
        }

        AddRelatedImgs();
    } 

    /****************************************************************************************/
    private void FindRelationsForSix()
    {
        foreach (var neighborSlot in neighborSlots)
        {
            OnePlusSlots.Add(neighborSlot);
        }

        OnePlusSlots.Add(neighborSlots[0]);
        for (var i = 0; i < OnePlusSlots.Count - 1; i++)
        {
            var s0 = OnePlusSlots[i];
            var s1 = OnePlusSlots[i + 1];
            CreateRelation(OnePlusSlots[i], OnePlusSlots[i + 1]);
        }
    }

    private void FindRelationForLess()
    {
        for (var i = 0; i < neighborSlots.Count - 1; i++)
        {
            var s0 = neighborSlots[i];
            var s1 = neighborSlots[i + 1];
            CreateRelation(neighborSlots[i], neighborSlots[i + 1]);
        }
    }
    /****************************************************************************************/

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

    private void AddRelatedImgs()
    {
        foreach (var relation in relations)
        {
            if (!RelatedSlots.IsContaining(relation.slot0)) RelatedSlots.Add(relation.slot0);
            if (!RelatedSlots.IsContaining(relation.slot1)) RelatedSlots.Add(relation.slot1);
        }

        if (RelatedSlots.Count > 1)
        {
            RelatedSlots.Add(gridManager.AllSlots[centerNo]);
        }

        if (relations.Count > 0)
        {
            result = true;
            DestroyImgs();
            GameManager.StartFalling();
            StartCoroutine(StartIe());

            IEnumerator StartIe()
            {
                yield return new WaitForSeconds(3);
                GameManager.StopFalling();
            }
        }

        // Log("___ :" + relations.Count);
    }

    private void DestroyImgs()
    {
        foreach (var slot in RelatedSlots)
        {
            Destroy(slot.img.gameObject);
        }
    }
    /****************************************************************************************/

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        gridManager = GameManager.GridManager;
        // GameManager.OnCheckMatching += CheckMatching;
        // GameManager.OnCalculate += CalculateForTriple;
    }

    private void OnDestroy()
    {
        // GameManager.OnCheckMatching -= CheckMatching;
        // GameManager.OnCalculate -= CalculateForTriple;
    }
}