using System;
using UnityEngine;

public class StatusEffectUI : MonoBehaviour
{

    [SerializeField]private Transform container;
    [SerializeField]private Transform statusEffectIcon;
    [SerializeField]private Unit thisUnit;

    private float delay=0;
    private bool statusEffectsChanged=false;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start() 
    {
        thisUnit.OnUnitStatusEffectChanged += Unit_OnUnitStatusEffectChanged;
    }

    private void UpdateVisual()
    {
        foreach (Transform item in container)
        {

            if (item != null)
            {
                Destroy(item.gameObject);
            }
        }

        foreach (StatusEffects item in thisUnit.GetComponents<StatusEffects>())
        {
            Transform statusEffectSingelTransform = Instantiate(statusEffectIcon, container);

            StatusEffectSingelUI statusEffectSingelUI = statusEffectSingelTransform.GetComponent<StatusEffectSingelUI>();

            statusEffectSingelUI.SetUp(item);
            
        }
        
    }

    private void Unit_OnUnitStatusEffectChanged(object sender, EventArgs e)
    {
        delay = 20;
        statusEffectsChanged = true;
        Debug.Log("Update visula Status Effect UI");
    }


    private void LateUpdate()
    {
        transform.forward = mainCamera.transform.forward;

        if (statusEffectsChanged)
        {
            if (delay <= 0)
            {
                UpdateVisual();
                statusEffectsChanged = false;
            }
            delay --;
        }
    }



}
