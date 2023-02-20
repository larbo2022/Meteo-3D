using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class KeybordCtrl : MonoBehaviour
{
    public float xAngle, yAngle;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void MoveCharacter(InputAction.CallbackContext ctx)
    {
        Vector2 angle = ctx.ReadValue<Vector2>();
        xAngle = angle.y;
        yAngle = angle.x;
        if (ctx.performed)
        {
            sphere.transform.Rotate(-xAngle, -yAngle, 0, Space.Self);
        }
        //sphere.transform.Rotate(-xAngle, -yAngle, 0, Space.Self);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
