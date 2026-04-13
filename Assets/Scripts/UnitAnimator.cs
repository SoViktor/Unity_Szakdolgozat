using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

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
            slashAction.OnStopSlashAction += SlashAction_OnStopSlashAction;
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
        animator.SetBool("StartSlash", true);

    }

        private void SlashAction_OnStopSlashAction(object sender, EventArgs e)
    {
        animator.SetBool("StartSlash", false);

    }


}
