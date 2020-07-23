using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    /****************************************************************************************/
    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
        RestartBtn.Initialize(RestartGame, true);
        GameManager.OnFinishGame += FinishGame;
    }

    private void OnDestroy()
    {
        GameManager.OnFinishGame -= FinishGame;
    }
}