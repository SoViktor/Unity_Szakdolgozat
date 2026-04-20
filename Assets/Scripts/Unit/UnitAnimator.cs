using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField]private Transform magicRayPrefab;
    [SerializeField]private Transform magicRayStartPoint;

    private void Awake()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoveAction += MoveAction_OnStartMoveAction;
            moveAction.OnStopMoveAction += MoveAction_OnStopMoveAction;

        }

        if (TryGetComponent<SlashAction>(out SlashAction slashAction))
        {
            slashAction.OnStartSlashAction += SlashAction_OnStartSlashAction;
        }

        if (TryGetComponent<MagicShootAction>(out MagicShootAction magicShootAction))
        {
            magicShootAction.OnStartMagicShootAction += MagicShootAction_OnStartMagicShootAction;

        }
    }
    private void MoveAction_OnStartMoveAction(object sender, EventArgs e)
    {
        animator.SetBool("IsRunning", true);
    }

    private void MoveAction_OnStopMoveAction(object sender, EventArgs e)
    {
        animator.SetBool("IsRunning", false);
    }

    private void SlashAction_OnStartSlashAction(object sender, EventArgs e)
    {
        animator.SetTrigger("StartSlash");

    }
    private void MagicShootAction_OnStartMagicShootAction(object sender, MagicShootAction.OnStartMagicShootActionArgs e)
    {
        animator.SetTrigger("StartSlash");
        Transform magicRayTransform = Instantiate(magicRayPrefab,magicRayStartPoint.position,Quaternion.identity);
        MagicRayVisual magicRayVisual = magicRayTransform.GetComponent<MagicRayVisual>();
        
        Vector3 targetUnitPosition = e.targetUnit.GetWorldPosition();

        targetUnitPosition.y = magicRayStartPoint.position.y;
        
        magicRayVisual.SetUp(targetUnitPosition);
    }

}
