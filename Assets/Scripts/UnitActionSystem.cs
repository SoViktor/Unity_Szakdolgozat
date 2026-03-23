using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance {get; private set;}
    public event EventHandler OnSelectedUnitChange;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("To Many UnitActionSystem! Only one Allowed" + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    private void Update()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            if(TryHandelUnitSelection()) return;
            selectedUnit.GetMoveAction().Move(MouseWorld.GetPosition());
        }
    }
    private bool TryHandelUnitSelection()
    {
        Ray ray =  Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue() );
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                SetSelectedUnit(unit);
                return true;
            }
        } 
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
        
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

}
