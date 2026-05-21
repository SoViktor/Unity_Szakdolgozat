using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{

    [SerializeField]private Transform ragdollRootSkeleton;

    public void SetUp(Transform unitRootSkeleton)
    {
        MatchAllTransforms(unitRootSkeleton, ragdollRootSkeleton);
    }

    private void MatchAllTransforms(Transform original, Transform copy)
    {
        copy.SetLocalPositionAndRotation(original.localPosition, original.localRotation);
        copy.localScale = original.localScale;

        for (int i = 0; i < original.childCount; i++)
        {
            MatchAllTransforms(original.GetChild(i), copy.GetChild(i));
        }
    }
}
