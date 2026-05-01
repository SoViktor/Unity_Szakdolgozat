using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthBarUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private Image fillImage;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void LateUpdate()
    {
        if (unit == null)
        {
            Destroy(gameObject);
            return;
        }

        float maxHP = unit.GetStatSystem().GetBaseHealthPoints();
        float currentHP = unit.GetStatSystem().GetHealthPoints();

        float ratio = currentHP / maxHP;

        fillImage.fillAmount = Mathf.Clamp01(ratio);
        transform.forward = mainCamera.transform.forward;
    }
}
