using UnityEngine;

public class GalaxyDemo : MonoBehaviour
{
    [Header("Objets")]
    [SerializeField] private Transform sun;
    [SerializeField] private Transform planet1;
    [SerializeField] private Transform planet2;
    [SerializeField] private Transform planet3;
    [SerializeField] private Transform moon;


    [Header("Axes de rotation sur soi-même")]
    [SerializeField] private Vector3 rotationAxisP1 = Vector3.up;
    [SerializeField] private Vector3 rotationAxisP2 = Vector3.up;
    [SerializeField] private Vector3 rotationAxisP3 = Vector3.up;
    [SerializeField] private Vector3 rotationAxisMoon = Vector3.up;


    [Header("Vitesses de rotation sur soi-même")]
    [SerializeField] private double rotationSpeedP1 = 0;
    [SerializeField] private double rotationSpeedP2 = 0;
    [SerializeField] private double rotationSpeedP3 = 0;
    [SerializeField] private double rotationSpeedMoon = 0;
    
    [Header("Axes d'orbite autour du soleil")]
    [SerializeField] private Vector3 orbitAxisP1 = Vector3.up;
    [SerializeField] private Vector3 orbitAxisP2 = Vector3.up;
    [SerializeField] private Vector3 orbitAxisP3 = Vector3.up;
    [SerializeField] private Vector3 orbitAxisMoon = Vector3.up;

    [Header("Vitesses d'orbite autour du soleil")]
    [SerializeField] private double orbitSpeedP1 = 10;
    [SerializeField] private double orbitSpeedP2 = 10;
    [SerializeField] private double orbitSpeedP3 = 10;
    [SerializeField] private double orbitSpeedMoon = 20;

    private Vector3Custom pivotPosition;

    private Vector3Custom initialPositionP1;
    private Vector3Custom initialPositionP2;
    private Vector3Custom initialPositionP3;
    private Vector3Custom initialMoonOffsetFromPlanet2;

    private double currentSelfAngleP1 = 0.0;
    private double currentSelfAngleP2 = 0.0;
    private double currentSelfAngleP3 = 0.0;
    private double currentSelfAngleMoon = 0.0;

    private double currentOrbitAngleP1 = 0.0;
    private double currentOrbitAngleP2 = 0.0;
    private double currentOrbitAngleP3 = 0.0;
    private double currentOrbitAngleMoon = 0.0;

    void Start()
    {
        if (sun == null || planet1 == null || planet2 == null || planet3 == null || moon == null)
        {
            enabled = false;
            return;
        }

        pivotPosition = ToCustomVector(sun.position);

        initialPositionP1 = ToCustomVector(planet1.position);
        initialPositionP2 = ToCustomVector(planet2.position);
        initialPositionP3 = ToCustomVector(planet3.position);
        initialMoonOffsetFromPlanet2 = ToCustomVector(moon.position).Subtract(ToCustomVector(planet2.position));
    }

    void Update()
    {
        UpdateSelfRotations();
        UpdateOrbits();
    }

    void UpdateSelfRotations()
    {
        currentSelfAngleP1 += rotationSpeedP1 * Time.deltaTime;
        currentSelfAngleP2 += rotationSpeedP2 * Time.deltaTime;
        currentSelfAngleP3 += rotationSpeedP3 * Time.deltaTime;
        currentSelfAngleMoon += rotationSpeedMoon * Time.deltaTime;

        ApplySelfRotationWithMatrix(planet1, rotationAxisP1, currentSelfAngleP1);
        ApplySelfRotationWithMatrix(planet2, rotationAxisP2, currentSelfAngleP2);
        ApplySelfRotationWithMatrix(planet3, rotationAxisP3, currentSelfAngleP3);
        ApplySelfRotationWithMatrix(moon, rotationAxisMoon, currentSelfAngleMoon);
    }

    void UpdateOrbits()
    {
        currentOrbitAngleP1 += orbitSpeedP1 * Time.deltaTime;
        currentOrbitAngleP2 += orbitSpeedP2 * Time.deltaTime;
        currentOrbitAngleP3 += orbitSpeedP3 * Time.deltaTime;
        currentOrbitAngleMoon += orbitSpeedMoon * Time.deltaTime;

        ApplyOrbitWithMatrix(planet1, initialPositionP1, orbitAxisP1, currentOrbitAngleP1);
        ApplyOrbitWithMatrix(planet2, initialPositionP2, orbitAxisP2, currentOrbitAngleP2);
        ApplyOrbitWithMatrix(planet3, initialPositionP3, orbitAxisP3, currentOrbitAngleP3);

        ApplyMoonOrbitWithMatrix();
    }

    void ApplySelfRotationWithMatrix(Transform target, Vector3 axis, double angle)
    {
        if (target == null || axis == Vector3.zero)
            return;

        Vector3Custom customAxis = ToCustomVector(axis);

        Matrix3x3 rotationMatrix = Matrix3x3.FromAxisAngle(
            customAxis,
            angle
        );

        Vector3Custom forward = new Vector3Custom(0, 0, 1);
        Vector3Custom up = new Vector3Custom(0, 1, 0);

        Vector3Custom rotatedForward = rotationMatrix.Multiply(forward);
        Vector3Custom rotatedUp = rotationMatrix.Multiply(up);

        Vector3 unityForward = ToUnityVector(rotatedForward);
        Vector3 unityUp = ToUnityVector(rotatedUp);

        target.rotation = Quaternion.LookRotation(unityForward, unityUp);
    }

    void ApplyOrbitWithMatrix(Transform planet, Vector3Custom initialPosition, Vector3 axis, double angle)
    {
        if (planet == null || axis == Vector3.zero)
            return;

        Vector3Custom customAxis = ToCustomVector(axis);

        Matrix3x3 orbitMatrix = Matrix3x3.FromAxisAngle(
            customAxis,
            angle
        );

        Vector3Custom newPosition = orbitMatrix.RotatePointAroundPivot(
            initialPosition,
            pivotPosition
        );

        planet.position = ToUnityVector(newPosition);
    }
    
    void ApplyMoonOrbitWithMatrix()
    {
        if (moon == null || planet2 == null || orbitAxisMoon == Vector3.zero)
            return;

        Vector3Custom customAxis = ToCustomVector(orbitAxisMoon);

        Matrix3x3 orbitMatrix = Matrix3x3.FromAxisAngle(
            customAxis,
            currentOrbitAngleMoon
        );

        Vector3Custom planet2CurrentPosition = ToCustomVector(planet2.position);

        Vector3Custom rotatedMoonOffset = orbitMatrix.Multiply(initialMoonOffsetFromPlanet2);

        Vector3Custom newMoonPosition = planet2CurrentPosition.Add(rotatedMoonOffset);

        moon.position = ToUnityVector(newMoonPosition);
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