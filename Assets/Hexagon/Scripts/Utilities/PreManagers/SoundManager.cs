using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : CustomBehaviour
{
    public AudioSource ClickAudioSource;
    public AudioClip[] ClickAudioClips;

    [Space(10)] public AudioSource GameStateAudioSource;
    public AudioClip[] GameStateAudioClips;

    [Space(10)] public AudioSource PlayerInteractionAudioSource;
    public AudioClip[] PlayerInteractionAudioClips;

    public bool IsSoundOn { get; set; }
    public bool IsVibrationOn { get; set; }

    private float mVibrationDelay = 0.25f;
    private int mVibrationRepeatRate = 3;

    public override void Initialize(GameManager gameManager)
    {
        base.Initialize(gameManager);
    }
}