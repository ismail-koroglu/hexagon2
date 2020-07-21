using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.Debug;

namespace Hexagon.Action
{
    public class InputManager : CustomBehaviour
    {
        public GameObject outline;
        public Transform HexBkg;
        public Slice SelectedSlice;
        private readonly List<Transform> ChildTripleHex = new List<Transform>();
        private float rotTime = .3f;

        /****************************************************************************************/

        private void RotateOutline()
        {
            DOTween.Kill(outline.transform);
            var rotZ = outline.transform.rotation.eulerAngles.z;
            outline.transform.DOLocalRotate(new Vector3(0, 0, 120), rotTime, RotateMode.LocalAxisAdd).OnComplete(() =>
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
            foreach (var item in ChildTripleHex)
            {
                item.SetParent(HexBkg);
            }

            ChildTripleHex.Clear();
        }

        private void ParentTripleToOutline()
        {
            foreach (var item in GameManager.GridManager.TripleNos)
            {
                GameManager.GridManager.AllSlots[item].transform.SetParent(outline.transform);
                ChildTripleHex.Add(GameManager.GridManager.AllSlots[item].transform);
            }
        }

        /****************************************************************************************/
        public override void Initialize(GameManager gameManager)
        {
            base.Initialize(gameManager);
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
                GameManager.StartRotation();
            }
        }
    }
}