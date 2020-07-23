using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

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
    public FallingManager FallingManager;
    public PointManager PointManager;

    public event Action OnSetTriple;
    public event Action OnStartRotation;
    public event Action OnStopRotation;
    public event Action OnCheckMatching;
    public event Action OnMatch;
    public event Action OnCalculate;
    public event Action OnStartFalling;
    public event Action OnStopFalling;
    public event Action<int, Transform> OnAddPoint;

    public bool IsRotating;
    public bool IsFalling;

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
        FallingManager.Initialize(this);
        PointManager.Initialize(this);

        AddPoint(0, null);
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

    public void Match()
    {
        OnMatch?.Invoke();
    }

    public void Calculate()
    {
        OnCalculate?.Invoke();
    }

    public void StartFalling()
    {
        IsFalling = true;
        OnStartFalling?.Invoke();
    }

    public void StopFalling()
    {
        IsFalling = false;
        OnStopFalling?.Invoke();
    }

    public void AddPoint(int point, Transform slot)
    {
        OnAddPoint?.Invoke(point, slot);
    }
}