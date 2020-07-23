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

        if (Input.GetMouseButtonDown(0))
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

        DetectSwipe();
    }

    public float minSwipeLength = 200f;
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    public static Swipe swipeDirection;

    public void DetectSwipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.magnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.None;
                    return;
                }

                currentSwipe.Normalize();

                if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Up;
                    Log("___ :" + "UP");
                }
                else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    swipeDirection = Swipe.Down;
                    Log("___ :" + "Down");
                }
                else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    swipeDirection = Swipe.Left;
                    Log("___ :" + "Left");
                }
                else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    swipeDirection = Swipe.Right;
                    Log("___ :" + "Right");
                }
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }
    }
}