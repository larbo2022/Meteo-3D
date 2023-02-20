using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static System.Math;

public class GetPosition : MonoBehaviour
{
    bool isClicked;
    Vector3 clickPos;
    Vector2 longLat;


    void Start()
    {
        
    }
    void OnMouseDown()
    {
        isClicked = Input.GetMouseButtonDown(0);
        clickPos = Input.mousePosition;

        RaycastHit rt;
        Ray ray = Camera.main.ScreenPointToRay(clickPos);
        if (Physics.Raycast(ray, out rt) && isClicked)
        {
            Debug.Log("Coordonnées cartésiennes : " + rt.point);
            // Transform into collider's local coordinate system.
            Vector3 offset = rt.collider.transform.InverseTransformPoint(rt.point);
            longLat = ToSpherical(offset);
        }
    }

    public Vector2 ToSpherical(Vector3 position)
    {
        // Convert to a unit vector so our y coordinate is in the range -1...1.
        position.Normalize();

        // The vertical coordinate (y) varies as the sine of latitude, not the cosine.
        float lat = Mathf.Asin(position.y) * Mathf.Rad2Deg;

        // Use the 2-argument arctangent, which will correctly handle all four quadrants.
        float lon = Mathf.Atan2(position.x, position.z) * Mathf.Rad2Deg;

        // Here I'm assuming (0, 0, 1) = 0 degrees longitude, and (1, 0, 0) = +90.
        // You can exchange/negate the components to get a different longitude convention.

        Debug.Log(lat + ", " + lon);

        // I usually put longitude first because I associate vector.x with "horizontal."
        return new Vector2(lon, lat);
    }

   
   
}
