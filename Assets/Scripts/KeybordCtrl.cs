using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class KeybordCtrl : MonoBehaviour
{
    public float xAngle, yAngle;
    public float speed = 20f;
    public GameObject sphere;


    public void MoveCharacter(InputAction.CallbackContext ctx)
    {
        Vector2 angle = ctx.ReadValue<Vector2>();
        xAngle = angle.y;
        yAngle = angle.x;
    }
    // Update is called once per frame
    void Update()
    {
        sphere.transform.Rotate(xAngle * speed * Time.deltaTime, -yAngle * speed * Time.deltaTime, 0, Space.World);
    }
}
