using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{

    [SerializeField] private Animator playerAnimnator;
    int isDeadHash;
    int isFinished;


    private void Awake()
    {
        isDeadHash = Animator.StringToHash("isDead");
        isFinished = Animator.StringToHash("isFinished");
    }

    public void SetDeathAnimation()
    {
        playerAnimnator.Play("isDead");
    }

    public void SetFinishAnimation()
    {
        playerAnimnator.Play("isFinished");
    }


}
