using System;
using UnityEngine;

public class RagdollCreator : MonoBehaviour
{
    [SerializeField] private Transform ragdollPrefab;
    [SerializeField] private Transform unitRootSkeleton;
    private StatSystem statSystem;

    private void Awake()
    {
        statSystem = GetComponent<StatSystem>();

        statSystem.OnDeath += StatSystem_OnDeath;
    }


    private void StatSystem_OnDeath(object sender, EventArgs e)
    {
        Transform ragdollTransform = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        UnitRagdoll unitRagdoll = ragdollTransform.GetComponent<UnitRagdoll>();
        unitRagdoll.SetUp(unitRootSkeleton);
    }
}
