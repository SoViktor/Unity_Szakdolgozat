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
        foreach (Transform child in original)
        {
            Transform copyChild = copy.Find(child.name);
            if (copyChild != null)
            {
                copyChild.position = child.position;
                copyChild.rotation = child.rotation;

                MatchAllTransforms(child,copyChild);
            }
        }
    }
}
