using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PointManager : CustomBehaviour
{
    public TextMeshProUGUI PointTm, FlyingTm;

    private void SetPoint(int point, Transform slot)
    {
        var lowValue = CurrentPoint;
        var highValue = CurrentPoint += point;

        DOTween.To(() => highValue, x => highValue = x, lowValue, 1).OnUpdate(() => PointTm.text = highValue.ToString());


        if (slot != null)
        {
            FlyingTm.text = "+" + point;
            FlyingTm.transform.position = slot.position;
            FlyingTm.gameObject.SetActive(true);
            FlyingTm.transform.DOMoveY(slot.transform.position.y + 200, 2).OnComplete(() => FlyingTm.gameObject.SetActive(false));
            FlyingTm.DOFade(1, 0);
            FlyingTm.DOFade(0, 2);
        }
    }


    /****************************************************************************************/
    public int CurrentPoint
    {
        get => PlayerPrefs.GetInt("CurrentPoint", 0);
        set
        {
            var tmp = PlayerPrefs.GetInt("CurrentPoint", 0);

            PlayerPrefs.SetInt("CurrentPoint", value);
        }
    }

    /****************************************************************************************/

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        GameManager.OnAddPoint += SetPoint;
    }

    private void OnDestroy()
    {
        GameManager.OnAddPoint -= SetPoint;
    }
}