using System;
using System.Collections;
using UnityEngine;

public class StatusEffectUI : MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds0_05 = new(0.05f);
    [SerializeField]private Transform container;
    [SerializeField]private Transform statusEffectIcon;
    [SerializeField]private Unit thisUnit;


    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start() 
    {
        StatusEffects.OnAnyStatusEffectApplied += StatusEffects_OnAnyStatusEffectApplied;
        StatusEffects.OnAnyStatusEffectRemoved += StatusEffects_OnAnyStatusEffectRemoved;
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

    private void StatusEffects_OnAnyStatusEffectApplied(object sender, EventArgsWithOneUnit e)
    {
        if (e.unit == thisUnit)
        {
            UpdateVisual();
        }
    }

    private void StatusEffects_OnAnyStatusEffectRemoved(object sender, EventArgsWithOneUnit e)
    {
        if (e.unit == thisUnit)
        {
            StartCoroutine(HandelStatusEffectIconRemoval());
        }
    }

    private IEnumerator HandelStatusEffectIconRemoval()
    {
        yield return _waitForSeconds0_05;
        UpdateVisual();
    }


    private void LateUpdate()
    {
        transform.forward = mainCamera.transform.forward;
    }

    private void OnDestroy()
    {
        StatusEffects.OnAnyStatusEffectApplied -= StatusEffects_OnAnyStatusEffectApplied;
        StatusEffects.OnAnyStatusEffectRemoved -= StatusEffects_OnAnyStatusEffectRemoved;

    }



}
