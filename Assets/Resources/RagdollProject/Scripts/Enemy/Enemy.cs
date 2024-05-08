using System;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Mover _mover;
    [SerializeField] private EnemyView _view;
    [SerializeField] private RagdollHandler _ragdollHandler;
    [SerializeField] private Transform controller;

    public void Initialize()
    {
        _view.Initialize(transform, controller);
        _ragdollHandler.Initialize(transform);
    }

    public void StartRun()
    {
        _view.StartRunning();
        //_mover.Enable();
    }

    public void Kill()
    {
        Debug.Log("Ќ≈≈≈“, € умееер! :(");

        EnableRagdollBehaviour();
    }

    public void TakeDamage(Vector3 force, Vector3 hitPoint)
    {
        Debug.Log("јй, больно!");
        EnableRagdollBehaviour();
        _ragdollHandler.Hit(force, hitPoint);
        StartCoroutine(TimeToWakeUP());
    }

    public void StandUp()
    {
        Debug.Log("”же встаю!");
        _ragdollHandler.Disable();
        _view.PlayStandingUp(_view.EnableAnimator, _view.StartIdling);
    }

    private void EnableRagdollBehaviour()
    {
        Debug.Log("ѕадаю!");
        _view.DisableAnimator();
        _mover.Disable();
        _ragdollHandler.Enable();
    }

    private IEnumerator TimeToWakeUP()
    {
        yield return new WaitForSeconds(4);
        StandUp();
    }
}
