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
            unit.GetMoveAction().GetValidGridPositionList();
        }
    }


}
