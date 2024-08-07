using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Rotator : MonoBehaviour
{
    public enum RotationAxis { X, Y }
    public RotationAxis rotationAxis = RotationAxis.X;
    public float rotationSpeed = 15f; 
    public bool rotateClockwise = true;

    private bool isSelected = false;

    public void Rotate()
    {
        isSelected = true;
    }

    public void StopRotate()
    {
        isSelected = false;
    }

    private void Update()
    {
        if (isSelected)
        {
            float angle = rotationSpeed * Time.deltaTime * (rotateClockwise ? 1 : -1);

            if (rotationAxis == RotationAxis.X)
            {
                transform.Rotate(Vector3.right, angle);
            }
            else if (rotationAxis == RotationAxis.Y)
            {
                transform.Rotate(Vector3.up, angle);
            }
        }
    }
}