using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
    private string IsRunningKey = "IsRunning";
    private string BackStandUpClipName = "BackStandUp";
    private string FrontStandClipName = "FrontStandUp";
    private string IsJumpingKey = "IsJumping";
    private string IsFlyingKey = "IsFlying";

    private int DefaultLayer = -1;

    private RigAdjusterForAnimation _rigAdjusterForBackStandingUpAnimation;
    private RigAdjusterForAnimation _rigAdjusterForFrontStandingUpAnimation;

    private Transform _parent;
    private Transform _controller;
    private Transform _hipsBone;

    private Animator _animator;

    private Action _standingUpCallback;

    private bool IsFrontUp => Vector3.Dot(_hipsBone.up, Vector3.up) > 0;

    public void Initialize(Transform parent, Transform controller)
    {
        _animator = GetComponent<Animator>();

        _parent = parent;
        _controller = controller;
        _hipsBone = _animator.GetBoneTransform(HumanBodyBones.Hips);

        AnimationClip[] currentClips = _animator.runtimeAnimatorController.animationClips;
        Transform[] bones = _hipsBone.GetComponentsInChildren<Transform>();

        _rigAdjusterForBackStandingUpAnimation = new RigAdjusterForAnimation(currentClips.First(clip => clip.name == BackStandUpClipName), bones, this);
        _rigAdjusterForFrontStandingUpAnimation = new RigAdjusterForAnimation(currentClips.First(clip => clip.name == FrontStandClipName), bones, this);
    }

    public void StartRunning() 
    {
        _animator.SetBool(IsRunningKey, true);
        _animator.SetBool(IsFlyingKey, false);   
    } 
    public void StartIdling()
    {
        _animator.SetBool(IsRunningKey, false);
        _animator.SetBool(IsFlyingKey, false);
       
    }
    public void StartJumping() => _animator.SetBool(IsJumpingKey, true);
    /*{
        _animator.SetBool(IsJumpingKey, true);
        //_animator.SetBool(IsFlyingKey, false);
    } */
    public void StartFlying()
    {
        _animator.SetBool(IsFlyingKey, true);
        Debug.Log("�������");
        Debug.Log($"isFlying - {_animator.GetBool(IsFlyingKey)}");
        //_animator.Play()
        
        //_animator.SetBool(IsJumpingKey, false);
        
       
    }


    public void PlayStandingUp(Action adjustAnimationEndedCallback = null, Action animationEndedCallback = null)
    {
        AdjustParentRotationToHipsBone();
        AdjustParentPositionToHipsBone();

        _standingUpCallback = animationEndedCallback;

        if (IsFrontUp)
            _rigAdjusterForFrontStandingUpAnimation.Adjust(() => CallbackForAdjustStandingUpAnimation(FrontStandClipName, adjustAnimationEndedCallback));
        else
            _rigAdjusterForBackStandingUpAnimation.Adjust(() => CallbackForAdjustStandingUpAnimation(BackStandUpClipName, adjustAnimationEndedCallback));
    }

    public void OnStandingUpAnimationEnded() => _standingUpCallback?.Invoke();

    public void DisableAnimator() => _animator.enabled = false;
    public void EnableAnimator() => _animator.enabled = true;

    private void CallbackForAdjustStandingUpAnimation(string clipName, Action additional�allback)
    {
        additional�allback?.Invoke();
        _animator.Play(clipName, DefaultLayer, 0f);
    }

    private void AdjustParentPositionToHipsBone()
    {
        Vector3 initHipsPosition = _hipsBone.position;
        _parent.position = initHipsPosition;
        //_controller.position = new Vector3(_parent.localPosition.x, 1f, _parent.localPosition.z);
        //_parent.position = new Vector3(0, 0, 0);
        AdjustParentPositionRelativeGround();

        _hipsBone.position = initHipsPosition;
    }

    private void AdjustParentPositionRelativeGround()
    {
        if (Physics.Raycast(_parent.position, Vector3.down, out RaycastHit hit, 5, 1 << LayerMask.NameToLayer(Layers.GroundLayerName)))
            _parent.position = new Vector3(_parent.position.x, hit.point.y, _parent.position.z);
            //_controller.position = new Vector3(_parent.localPosition.x, 1f, _parent.localPosition.z);

    }

    private void AdjustParentRotationToHipsBone()
    {
        Vector3 initHipsPosition = _hipsBone.position;
        Quaternion initHipsRotation = _hipsBone.rotation;

        Vector3 directionForRotate = _hipsBone.up;

        if (IsFrontUp == false)
            directionForRotate *= -1;

        directionForRotate.y = 0;

        Quaternion correctionRotation = Quaternion.FromToRotation(_parent.forward, directionForRotate.normalized);
        _parent.rotation *= correctionRotation;
        //_controller.rotation = _parent.rotation;
        _hipsBone.position = initHipsPosition;
        _hipsBone.rotation = initHipsRotation;
    }
}
