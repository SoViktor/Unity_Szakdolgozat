using UnityEngine;
using UnityEngine.InputSystem;

public class Testing : MonoBehaviour
{
    [SerializeField]private Unit unit;

    void Start()
    {

        
    }

    private void Update()
    {
        if (Keyboard.current.tKey.isPressed)
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(unit.GetMoveAction().GetValidGridPositionList());
        }
    }


}
