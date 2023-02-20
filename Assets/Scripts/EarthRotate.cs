using UnityEngine;

public class EarthRotate : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    public GameObject sphere;

    // Update is called once per frame
    void Update()
    {
        sphere.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    }
}
