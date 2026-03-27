using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance {get; private set;}
    public event EventHandler OnSelectedUnitChange;
    public event EventHandler OnSelectedActionChange;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;

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

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }
    private void Update()
    {
        if (isBusy)
        {
            return;
        }

        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (TryHandelUnitSelection())
        {
            return;
        }

        HandelSelectedAction();

    }

    private void HandelSelectedAction()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
            {
                if (selectedUnit.TryTakeActionPointFofAction(selectedAction))
                {
                    SetBusy();
                    selectedAction.TakeAction(mouseGridPosition, ClearBusy);
                    OnActionStarted?.Invoke(this, EventArgs.Empty);  
                }    
            }
        }
    }

    private void SetBusy()
    {
        isBusy = true;

        OnBusyChanged?.Invoke(this, isBusy);
    }
    
    private void ClearBusy()
    {
        isBusy = false;

        OnBusyChanged?.Invoke(this, isBusy);

    }
    
    private bool TryHandelUnitSelection()
    {
        if (Mouse.current.leftButton.IsPressed())
        {
            Ray ray =  Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue() );
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
            {
                if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == selectedUnit)
                    {
                        return false;
                    }
                    SetSelectedUnit(unit);
                    return true;
                }
            } 
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());

        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
        
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;

        OnSelectedActionChange?.Invoke(this, EventArgs.Empty);    
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }

    public BaseAction GetSelectedAction()
    {
        return selectedAction;
    }
}
