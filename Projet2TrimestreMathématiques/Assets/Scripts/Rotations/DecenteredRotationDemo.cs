using UnityEngine;

public class DecenteredRotationDemo : MonoBehaviour
{
    [Header("Objets")]
    [SerializeField] private Transform objectWithQuaternion;
    [SerializeField] private Transform objectWithMatrix;
    [SerializeField] private Transform pivotObject;

    [Header("Rotation")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;
    [SerializeField] private double rotationSpeed = 45.0;

    private Vector3Custom initialQuaternionPosition;
    private Vector3Custom initialMatrixPosition;
    private Vector3Custom pivotPosition;

    private double currentAngle = 0.0;

    void Start()
    {
        if (objectWithQuaternion == null || objectWithMatrix == null || pivotObject == null)
        {
            Debug.LogError("Assigne les deux objets et le pivot dans l'Inspector.");
            enabled = false;
            return;
        }

        initialQuaternionPosition = ToCustomVector(objectWithQuaternion.position);
        initialMatrixPosition = ToCustomVector(objectWithMatrix.position);
        pivotPosition = ToCustomVector(pivotObject.position);
    }

    void Update()
    {
        if (rotationAxis == Vector3.zero)
            return;

        currentAngle += rotationSpeed * Time.deltaTime;

        Vector3Custom customAxis = ToCustomVector(rotationAxis);

        QuaternionCustom rotationQuaternion = QuaternionCustom.FromAxisAngle(
            customAxis,
            currentAngle
        );

        Matrix3x3 rotationMatrix = Matrix3x3.FromAxisAngle(
            customAxis,
            currentAngle
        );

        Vector3Custom newQuaternionPosition =
            rotationQuaternion.RotatePointAroundPivot(initialQuaternionPosition, pivotPosition);

        Vector3Custom newMatrixPosition =
            rotationMatrix.RotatePointAroundPivot(initialMatrixPosition, pivotPosition);

        objectWithQuaternion.position = ToUnityVector(newQuaternionPosition);
        objectWithMatrix.position = ToUnityVector(newMatrixPosition);
    }

    Vector3Custom ToCustomVector(Vector3 vector)
    {
        return new Vector3Custom(vector.x, vector.y, vector.z);
    }

    Vector3 ToUnityVector(Vector3Custom vector)
    {
        return new Vector3(
            (float)vector.X,
            (float)vector.Y,
            (float)vector.Z
        );
    }
}