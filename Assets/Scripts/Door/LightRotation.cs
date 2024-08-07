using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;

    private bool clockwise;
    private bool isSelected;

    public void IsClockwise()
    {
        isSelected = true;
        clockwise = true;
    }

    public void NotClockwise()
    {
        isSelected = true;
        clockwise = false;
    }

    public void ExitSelect()
    {
        isSelected = false;
    }

    void Update()
    {
        if (isSelected)
        {
            if (clockwise)
            {
                float angle = rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.right, angle);
            }
            else
            {
                float angle = -rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.right, angle);
            }
        }
    }
}