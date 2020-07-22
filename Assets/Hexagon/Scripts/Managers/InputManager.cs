﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
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
        var rotationAmount = (int) GameManager.GridManager.rotateType * 120;
        outline.transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), rotateDuration, RotateMode.LocalAxisAdd).OnComplete(() =>
        {
            if (!GameManager.Calculator.IsMatching())
            {
                outline.transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), rotateDuration, RotateMode.LocalAxisAdd).OnComplete(() =>
                {
                    if (!GameManager.Calculator.IsMatching())
                    {
                        outline.transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), rotateDuration, RotateMode.LocalAxisAdd).OnComplete(() =>
                        {
                            GameManager.Calculator.IsMatching();
                            GameManager.StopRotation();
                        });
                    }
                    else
                    {
                        GameManager.StopRotation();
                    }
                });
                // GameManager.StopRotation();
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

    private void UnparentTriple()
    {
        foreach (var img in tripleImgs)
        {
            if (img) img.SetParent(gridManager.imgBkg);
        }
    }

    private void ParentTriple()
    {
        tripleImgs.Clear();
        foreach (var no in GameManager.GridManager.TripleNos)
        {
            var img = gridManager.AllSlots[no].img;
            if (!img) return;
            img.transform.SetParent(outline.transform);
            tripleImgs.Add(img.transform);
        }
    }

    private void StopRotation()
    {
        DOTween.Kill(outline.transform);
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        gridManager = GameManager.GridManager;
        outline.GetComponent<HexOutline>().Initialize(GameManager);
        GameManager.OnStartRotation += RotateOutline;
        GameManager.OnMatch += HideOutline;
        GameManager.OnStopRotation += StopRotation;
    }

    private void OnDestroy()
    {
        GameManager.OnStartRotation -= RotateOutline;
        GameManager.OnMatch -= HideOutline;
        GameManager.OnStopRotation -= StopRotation;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.IsRotating)
        {
            var objUnderMouse = GetCollider();
            if (objUnderMouse == null) return;
            SelectedSlice = objUnderMouse.GetMappedSlice();
            UnparentTriple();
            GameManager.SetTriple();
            outline.transform.position = SelectedSlice.transform.position;
            outline.transform.rotation = Quaternion.Euler(SelectedSlice.RotVector3);
            outline.transform.SetAsLastSibling();
            ParentTriple();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && !GameManager.IsRotating)
        {
            GameManager.GridManager.rotateType = RotateType.Cw;
            GameManager.StartRotation();
        }

        else if (Input.GetKeyDown(KeyCode.W) && !GameManager.IsRotating)
        {
            GameManager.GridManager.rotateType = RotateType.Ccw;
            GameManager.StartRotation();
        }


        else if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Calculate();
        }
    }
}