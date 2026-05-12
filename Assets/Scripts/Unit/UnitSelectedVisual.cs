using System;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField]private Unit unit;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        UpdateVisual();
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs empty)
    {
        UpdateVisual();
    }
    private void UpdateVisual()
    {        
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
        {
            meshRenderer.enabled = true;
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }

    private void OnDestroy()
    {
        TurnSystem.Instance.OnTurnChanged -=TurnSystem_OnTurnChanged;

    }
}
