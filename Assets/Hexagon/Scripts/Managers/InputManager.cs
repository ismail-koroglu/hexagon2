using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
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
        StartCoroutine(StartIe0());
        StartCoroutine(StartIe());

        IEnumerator StartIe()
        {
            yield return new WaitForSeconds(1);
            GameManager.StopRotation();
        }
    }

    IEnumerator StartIe0()
    {
        yield return tween().WaitForCompletion();

        if (!GameManager.Calculator.IsMatching())
        {
            yield return tween().WaitForCompletion();

            if (!GameManager.Calculator.IsMatching())
            {
                yield return tween().WaitForCompletion();
                GameManager.Calculator.IsMatching();
            }
        }
    }

    private Tween tween()
    {
        var rotationAmount = (int) GameManager.GridManager.rotateType * 120;
        return outline.transform.DOLocalRotate(new Vector3(0, 0, rotationAmount), rotateDuration, RotateMode.LocalAxisAdd);
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
        GameManager.OnStopRotation += StopRotation;
    }

    private void OnDestroy()
    {
        GameManager.OnStartRotation -= RotateOutline;
        GameManager.OnStopRotation -= StopRotation;
    }

    private void Update()
    {
        if (GameManager.IsRotating || GameManager.IsFalling || GameManager.IsGameFinished) return;

        if (Input.GetMouseButtonUp(0))
        {
            var objUnderMouse = GetCollider();
            if (objUnderMouse == null) return;
            SelectedSlice = objUnderMouse.GetMappedSlice();
            UnparentTriple();
            GameManager.SetTriple();
            outline.transform.position = SelectedSlice.transform.position;
            outline.transform.rotation = Quaternion.Euler(SelectedSlice.RotVector3);
            // outline.transform.SetAsLastSibling();
            ParentTriple();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameManager.GridManager.rotateType = RotateType.Ccw;
            GameManager.StartRotation();
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            GameManager.GridManager.rotateType = RotateType.Cw;
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

    public void RoteteN()
    {
        GameManager.GridManager.rotateType = RotateType.Ccw;
        GameManager.StartRotation();
    }

    public void RoteteS()
    {
        GameManager.GridManager.rotateType = RotateType.Cw;
        GameManager.StartRotation();
    }

    public void RoteteW()
    {
        GameManager.GridManager.rotateType = RotateType.Ccw;
        GameManager.StartRotation();
    }

    public void RoteteE()
    {
        GameManager.GridManager.rotateType = RotateType.Cw;
        GameManager.StartRotation();
    }
}