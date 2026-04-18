using UnityEngine;

public class MagicRayVisual : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField]private Transform magicRayVFX;
    
    private Vector3 targetPosition;
    private float timer;
    public void SetUp(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
        timer = delay;
    }

    private void Update() 
    {
        timer -=Time.deltaTime;
        if (timer > 0)
        {
            return;
        }
        Vector3 moveDirection = (targetPosition -transform.position).normalized;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        float raySpeed = 100f;

        transform.position += moveDirection * raySpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceAfterMoving > distanceBeforeMoving)
        {
            Instantiate(magicRayVFX, targetPosition, Quaternion.identity);
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);
        }

    }
}
