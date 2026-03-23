using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalFloatAmount;
    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0,spinAddAmount,0);
        totalFloatAmount += spinAddAmount;
        if (totalFloatAmount >= 360)
        {
            isActive = false;
        }
    }

    public void Spin()
    {
        isActive = true;
        totalFloatAmount = 0;
    }
}
