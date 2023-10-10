using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public enum Axis
    {
        X,
        Y,
        Z
    }
    public Transform parentTransform;
    public Axis rotationAxis;
    public float rotationSpeed;

    void Update()
    {
        // Get the local rotation of the object.
        Quaternion localRotation = transform.localRotation;

        // Rotate the rotation around the specified axis.
        switch (rotationAxis)
        {
            case Axis.X:
                localRotation *= Quaternion.Euler(rotationSpeed * Time.deltaTime, 0f, 0f);
                break;
            case Axis.Y:
                localRotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
                break;
            case Axis.Z:
                localRotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
                break;
        }

        // Set the local rotation of the object.
        transform.localRotation = localRotation;
    }
}
