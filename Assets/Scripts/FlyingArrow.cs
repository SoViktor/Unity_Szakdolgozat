using UnityEngine;

public class FlyingArrow : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField]private Transform shootHitVFX;
    
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

        transform.rotation = Quaternion.LookRotation(moveDirection);
        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        float arrowSpeed = 10f;

        transform.position += moveDirection * arrowSpeed * Time.deltaTime;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceAfterMoving > distanceBeforeMoving)
        {
            Instantiate(shootHitVFX, targetPosition, Quaternion.identity);
            transform.position = targetPosition;
            Destroy(gameObject);
        }

    }
}
