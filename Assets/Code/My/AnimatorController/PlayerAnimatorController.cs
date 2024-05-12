using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    [SerializeField] private Animator playerAnimnator;
    //[SerializeField] private Animation deathAnimation;
    int isDeadHash;
    int isFinished;


    private void Awake()
    {
        isDeadHash = Animator.StringToHash("isDead");
        isFinished = Animator.StringToHash("isFinished");
    }

    public void SetDeathAnimation()
    {
        //playerAnimnator.SetBool(isDeadHash, true);
        playerAnimnator.Play("isDead");
    }

    public void SetFinishAnimation()
    {
        //playerAnimnator.SetBool(isFinished, true);
        playerAnimnator.Play("isFinished");
    }


}
