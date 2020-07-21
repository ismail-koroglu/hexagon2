using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.Debug;


public class InputManager : CustomBehaviour
{
    public GameObject outline;

    public Slice SelectedSlice;
    public List<Transform> tripleImgs = new List<Transform>();
    private const float rotateDuration = .3f;

    private GridManager gridManager;

    /****************************************************************************************/

    private void RotateOutline()
    {
        DOTween.Kill(outline.transform);
        var rotateType = (int) GameManager.GridManager.rotateType;
        outline.transform.DOLocalRotate(new Vector3(0, 0, rotateType * 120), rotateDuration, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            if (!GameManager.Calculator.IsMatching())
            {
                // outline.transform.DOLocalRotate(new Vector3(0, 0, 120), rotTime, RotateMode.LocalAxisAdd).OnComplete(() =>
                // {
                //     if (!GameManager.Calculator.IsMatching())
                //     {
                //         outline.transform.DOLocalRotate(new Vector3(0, 0, 120), rotTime, RotateMode.LocalAxisAdd).OnComplete(() =>
                //             GameManager.StopRotation());
                //     }
                //     else
                //     {
                //         GameManager.StopRotation();
                //     }
                // });
                GameManager.StopRotation();
            }
            else
            {
                GameManager.StopRotation();
            }
        });
    }

    /****************************************************************************************/
    private Slice GetCollider()
    {
        var clickedObject = GetObjUnderMouse();
        if (clickedObject == null || !clickedObject.GetComponent<Slice>()) return null;
        return clickedObject.GetComponent<Slice>();
    }

    private GameObject GetObjUnderMouse()
    {
        var worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        return hit ? hit.collider.gameObject : null;
    }

    private void HideOutline()
    {
        outline.SetActive(false);
    }

    private void ParentTripleToBkg()
    {
        foreach (var img in tripleImgs)
        {
            img.SetParent(gridManager.imgBkg);
        }

        tripleImgs.Clear();
    }

    private void ParentTripleToOutline()
    {
        foreach (var no in GameManager.GridManager.TripleNos)
        {
            var img = gridManager.AllSlots[no].img;
            img.transform.SetParent(outline.transform);
            tripleImgs.Add(GameManager.GridManager.AllImgs[no].transform);
        }
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        gridManager = GameManager.GridManager;
        outline.GetComponent<HexOutline>().Initialize(GameManager);
        GameManager.OnStartRotation += RotateOutline;
        GameManager.OnMatch += HideOutline;
    }

    private void OnDestroy()
    {
        GameManager.OnStartRotation -= RotateOutline;
        GameManager.OnMatch -= HideOutline;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.IsRotating)
        {
            var objUnderMouse = GetCollider();
            if (objUnderMouse == null) return;
            SelectedSlice = objUnderMouse.GetMappedSlice();
            GameManager.SelectHex();
            ParentTripleToBkg();
            outline.transform.position = SelectedSlice.transform.position;
            outline.transform.rotation = Quaternion.Euler(SelectedSlice.RotVector3);
            outline.transform.SetAsLastSibling();
            ParentTripleToOutline();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !GameManager.IsRotating)
        {
            GameManager.GridManager.rotateType = RotateType.Cw;
            GameManager.StartRotation();
        }
    }
}