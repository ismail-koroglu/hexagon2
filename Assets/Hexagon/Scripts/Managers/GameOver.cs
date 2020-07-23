using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameOver : CustomBehaviour
{
    public GameObject FinishPanel;

    public CustomButton RestartBtn;

    /****************************************************************************************/

    private void FinishGame()
    {
        FinishPanel.SetActive(true);
        GameManager.IsGameFinished = true;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("Main");
    }

    private void StartExplosion()
    {
        var counter = 0;
        var bomb = GameManager.GridManager.Bomb;
        GameManager.InputManager.outline.SetActive(false);
        foreach (var img in GameManager.GridManager.AllImgs)
        {
            // Log("___ :" + counter++);

            if (img != null)
            {
                var div = Utilities.Vector3Dir(bomb.transform.position, img.transform.position);
                img.transform.DOMove(div * Random.Range(400, 800), .25f).SetEase(Ease.OutSine);
            }
        }

        StartCoroutine(StartIe());

        IEnumerator StartIe()
        {
            yield return new WaitForSeconds(1);
            FinishGame();
        }
    }

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        RestartBtn.Initialize(RestartGame, true);
        GameManager.OnFinishGame += StartExplosion;
    }

    private void OnDestroy()
    {
        GameManager.OnFinishGame -= StartExplosion;
    }
}