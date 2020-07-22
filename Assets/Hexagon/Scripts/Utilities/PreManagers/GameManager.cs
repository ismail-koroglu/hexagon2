using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : CustomBehaviour
{
    public GridManager GridManager;
    public InputManager InputManager;
    public LevelManager LevelManager;
    public SoundManager SoundManager;
    public UIManager UIManager;
    public Constants Constants;
    public SelectionManager SelectionManager;
    public Calculator Calculator;
    public ImgManager ImgManager;

    public event Action OnStartGame;
    public event Action OnCountdownFinished;
    public event Action OnGameFinished;
    public event Action OnRestartGame;
    public event Action OnResumeGame;
    public event Action OnResetToMainMenu;
    public event Action OnLevelCompleted;
    public event Action OnLevelFailed;
    public event Action OnSetTriple;
    public event Action OnStartRotation;
    public event Action OnStopRotation;
    public event Action OnCheckMatching;
    public event Action OnMatch;
    public event Action OnCalculate;

    public bool IsRotating;

    public void Awake()
    {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GridManager.Initialize(this);
        InputManager.Initialize(this);
        LevelManager.Initialize(this);
        SoundManager.Initialize(this);
        UIManager.Initialize(this);
        Constants.Initialize(this);
        SelectionManager.Initialize(this);
        Calculator.Initialize(this);
        ImgManager.Initialize(this);
    }

    public void StartGame()
    {
        OnStartGame?.Invoke();
    }

    public void CountdownFinished()
    {
        OnCountdownFinished?.Invoke();
    }

    public void GameFinished()
    {
        OnGameFinished?.Invoke();
    }

    public void ResetToMainMenu()
    {
        OnResetToMainMenu?.Invoke();
    }

    public void RestartGame()
    {
        OnRestartGame?.Invoke();
    }

    public void ResumeGame()
    {
        OnResumeGame?.Invoke();
    }

    public void LevelCompleted()
    {
        OnLevelCompleted?.Invoke();
    }

    public void LevelFailed()
    {
        OnLevelFailed?.Invoke();
    }

    public void SetTriple()
    {
        OnSetTriple?.Invoke();
    }

    public void StartRotation()
    {
        OnStartRotation?.Invoke();
        IsRotating = true;
    }

    public void StopRotation()
    {
        IsRotating = false;
        OnStopRotation?.Invoke();
    }

    public void CheckMatching()
    {
        OnCheckMatching?.Invoke();
    }

    public void Match()
    {
        OnMatch?.Invoke();
    }

    public void Calculate()
    {
        OnCalculate?.Invoke();
    }
}