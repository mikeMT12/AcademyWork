using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAndMusicSystem : MonoBehaviour
{
    [Header("Sounds")]
    public AudioSource countdownNums;
    public AudioSource countdownGo;

    public AudioSource winSound;
    public AudioSource loseSound;

    public AudioSource jump;
    public AudioSource getDamage;

    [Header("Music")]
    public AudioSource inGameMusic;
    public AudioSource startGameMusic;
    //[SerializeField] AudioClip pauseGameMusic;

}
