using System;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
    private static readonly int StartShootHash = Animator.StringToHash("StartShoot");
    private static readonly int StartSlashHash = Animator.StringToHash("StartSlash");
    [SerializeField] private Animator animator;
    [SerializeField]private Transform magicRayPrefab;
    [SerializeField]private Transform shootStartPoint;
    [SerializeField]private Transform arrowPrefab;
    [SerializeField] private Transform summonVFX;

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

        if (TryGetComponent<ArrowShootAction>(out ArrowShootAction arrowShootAction))
        {
            arrowShootAction.OnStartArrowShootAction += ArrowShootAction_OnStartArrowShootAction;
        }

        if (TryGetComponent<SummonAction>(out SummonAction summonAction))
        {
            summonAction.OnNewUnitSummoned += SummonAction_OnNewUnitSummoned;
        }

        if (TryGetComponent<HealAction>(out HealAction healAction))
        {
            healAction.OnStartHeal += HealAction_OnStartHeal;
        }

        if (TryGetComponent<BuffAction>(out BuffAction buffAction))
        {
            buffAction.OnBuffActionStarted += BuffAction_OnBuffActionStarted;
        }
        if (TryGetComponent<DebuffAction>(out DebuffAction debuffAction))
        {
            debuffAction.OnDebuffActionStarted += DebuffAction_OnDebuffActionStarted;
        }

        if (TryGetComponent<DoTAction>(out DoTAction doTAction))
        {
            doTAction.OnDoTActionStarted += DoTAction_OnDoTActionStarted;
        }
    }
    private void MoveAction_OnStartMoveAction(object sender, EventArgs e)
    {
        animator.SetBool(IsRunningHash, true);
    }

    private void MoveAction_OnStopMoveAction(object sender, EventArgs e)
    {
        animator.SetBool(IsRunningHash, false);
    }

    private void SlashAction_OnStartSlashAction(object sender, EventArgs e)
    {
        animator.SetTrigger(StartSlashHash);

    }
    private void MagicShootAction_OnStartMagicShootAction(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartSlashHash);
        Transform magicRayTransform = Instantiate(magicRayPrefab,shootStartPoint.position,Quaternion.identity);
        MagicRayVisual magicRayVisual = magicRayTransform.GetComponent<MagicRayVisual>();
        
        Vector3 targetUnitPosition = e.TargetUnit.GetWorldPosition();

        targetUnitPosition.y = shootStartPoint.position.y;
        
        magicRayVisual.SetUp(targetUnitPosition);
    }

    private void ArrowShootAction_OnStartArrowShootAction(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartShootHash);
        Transform arrowTransform =Instantiate(arrowPrefab,shootStartPoint.position, Quaternion.identity);
        FlyingArrow flyingArrow = arrowTransform.GetComponent<FlyingArrow>();

        Vector3 targetUnitPosition = e.TargetUnit.GetWorldPosition();

        targetUnitPosition.y = shootStartPoint.position.y;

        flyingArrow.SetUp(targetUnitPosition);
    }

    private void SummonAction_OnNewUnitSummoned(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartSlashHash);

        Vector3 targetPosition = e.TargetUnit.GetWorldPosition();

        //targetPosition.y = shootStartPoint.position.y;

        Instantiate(summonVFX, targetPosition, Quaternion.identity);


    }

    private void HealAction_OnStartHeal(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartSlashHash);

        Vector3 targetPosition = e.TargetUnit.GetWorldPosition();

        targetPosition.y = shootStartPoint.position.y;

        Instantiate(summonVFX, targetPosition, Quaternion.identity);
    }

    private void BuffAction_OnBuffActionStarted(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartSlashHash);
        Vector3 targetPosition = e.TargetUnit.GetWorldPosition();

        targetPosition.y = shootStartPoint.position.y;

        Instantiate(summonVFX, targetPosition, Quaternion.identity);

    }

    private void DebuffAction_OnDebuffActionStarted(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartSlashHash);
        Vector3 targetPosition = e.TargetUnit.GetWorldPosition();

        targetPosition.y = shootStartPoint.position.y;

        Instantiate(summonVFX, targetPosition, Quaternion.identity);
    }

    private void DoTAction_OnDoTActionStarted(object sender, ActionArgsWithTwoUnits e)
    {
        animator.SetTrigger(StartSlashHash);
        Vector3 targetPosition = e.TargetUnit.GetWorldPosition();

        targetPosition.y = shootStartPoint.position.y;

        Instantiate(summonVFX, targetPosition, Quaternion.identity);
    } 

    private void OnDestroy()
    {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction))
        {
            moveAction.OnStartMoveAction -= MoveAction_OnStartMoveAction;
            moveAction.OnStopMoveAction -= MoveAction_OnStopMoveAction;

        }

        if (TryGetComponent<SlashAction>(out SlashAction slashAction))
        {
            slashAction.OnStartSlashAction -= SlashAction_OnStartSlashAction;
        }

        if (TryGetComponent<MagicShootAction>(out MagicShootAction magicShootAction))
        {
            magicShootAction.OnStartMagicShootAction -= MagicShootAction_OnStartMagicShootAction;

        }

        if (TryGetComponent<ArrowShootAction>(out ArrowShootAction arrowShootAction))
        {
            arrowShootAction.OnStartArrowShootAction -= ArrowShootAction_OnStartArrowShootAction;
        }

        if (TryGetComponent<SummonAction>(out SummonAction summonAction))
        {
            summonAction.OnNewUnitSummoned -= SummonAction_OnNewUnitSummoned;
        }

        if (TryGetComponent<HealAction>(out HealAction healAction))
        {
            healAction.OnStartHeal -= HealAction_OnStartHeal;
        }

        if (TryGetComponent<BuffAction>(out BuffAction buffAction))
        {
            buffAction.OnBuffActionStarted -= BuffAction_OnBuffActionStarted;
        }
        if (TryGetComponent<DebuffAction>(out DebuffAction debuffAction))
        {
            debuffAction.OnDebuffActionStarted -= DebuffAction_OnDebuffActionStarted;
        }

        if (TryGetComponent<DoTAction>(out DoTAction doTAction))
        {
            doTAction.OnDoTActionStarted -= DoTAction_OnDoTActionStarted;
        }
    }

}
