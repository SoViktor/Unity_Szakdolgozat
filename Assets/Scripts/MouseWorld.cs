using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask mousePlaneLayerMask;
    private static MouseWorld instance;

    void Awake()
    {
        instance = this;
    }
/*     void Update()
    {
        transform.position = MouseWorld.GetPosition();
    } */
    public static Vector3 GetPosition()
    {
        Ray ray =  Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue() );
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask );
        return raycastHit.point;
 
    }
}
