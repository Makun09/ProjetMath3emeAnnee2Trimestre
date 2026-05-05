using UnityEngine;

public class RotationComparisonDemo : MonoBehaviour
{
    
    private bool hasLoggedOperationComparison = false;
    
    [Header("Cubes")]
    public Transform cubeQuaternion;
    public Transform cubeMatrix;

    [Header("Rotation")]
    public double rotationSpeed = 45.0;
    public Vector3Custom rotationAxis = new Vector3Custom(0, 1, 0);

    [Header("Logs opérations")]
    public bool showOperationLogs = true;
    public float logInterval = 2.0f;

    private double currentAngle = 0.0;
    private float logTimer = 0.0f;

    void Update()
    {
        currentAngle += rotationSpeed * Time.deltaTime;

        QuaternionCustom rotationQuaternion = QuaternionCustom.FromAxisAngle(rotationAxis, currentAngle);
        Matrix3x3 rotationMatrix = rotationQuaternion.ToRotationMatrix3x3();

        RotateWithQuaternion(rotationQuaternion);
        RotateWithMatrix(rotationMatrix);

        if (showOperationLogs && !hasLoggedOperationComparison)
        {
            logTimer += Time.deltaTime;

            if (logTimer >= logInterval)
            {
                LogOperationComparison();
                hasLoggedOperationComparison = true;
            }
        }
    }

    void RotateWithQuaternion(QuaternionCustom q)
    {
        if (cubeQuaternion == null)
            return;

        cubeQuaternion.rotation = new Quaternion(
            (float)q.X,
            (float)q.Y,
            (float)q.Z,
            (float)q.W
        );
    }

    void RotateWithMatrix(Matrix3x3 rotationMatrix)
    {
        if (cubeMatrix == null)
            return;

        Vector3Custom forward = new Vector3Custom(0, 0, 1);
        Vector3Custom up = new Vector3Custom(0, 1, 0);

        Vector3Custom rotatedForward = rotationMatrix.Multiply(forward);
        Vector3Custom rotatedUp = rotationMatrix.Multiply(up);

        Vector3 unityForward = new Vector3(
            (float)rotatedForward.X,
            (float)rotatedForward.Y,
            (float)rotatedForward.Z
        );

        Vector3 unityUp = new Vector3(
            (float)rotatedUp.X,
            (float)rotatedUp.Y,
            (float)rotatedUp.Z
        );

        cubeMatrix.rotation = Quaternion.LookRotation(unityForward, unityUp);
    }

    void LogOperationComparison()
    {
        OperationCounter quaternionPointOperations = CountQuaternionRotationForOnePoint();
        OperationCounter matrixPointOperations = CountMatrixRotationForOnePoint();

        OperationCounter quaternionCubeOperations = quaternionPointOperations.MultiplyBy(8);
        OperationCounter matrixCubeOperations = matrixPointOperations.MultiplyBy(8);

        Debug.Log("=== Comparaison du nombre d'opérations ===");
        Debug.Log("Angle actuel = " + currentAngle + "°");

        Debug.Log("Cube Quaternion - rotation d'un point : " + quaternionPointOperations);
        Debug.Log("Cube Matrix - rotation d'un point : " + matrixPointOperations);

        Debug.Log("Cube Quaternion - estimation pour 8 sommets : " + quaternionCubeOperations);
        Debug.Log("Cube Matrix - estimation pour 8 sommets : " + matrixCubeOperations);
    }

    OperationCounter CountQuaternionRotationForOnePoint()
    {
        OperationCounter counter = new OperationCounter();


        counter.Multiplications += 32;
        counter.AdditionsSubtractions += 24;

        counter.SignChanges += 3;

        return counter;
    }

    OperationCounter CountMatrixRotationForOnePoint()
    {
        OperationCounter counter = new OperationCounter();
        
        counter.Multiplications += 9;
        counter.AdditionsSubtractions += 6;

        return counter;
    }

    private class OperationCounter
    {
        public int Multiplications;
        public int AdditionsSubtractions;
        public int SignChanges;

        public int Total()
        {
            return Multiplications + AdditionsSubtractions + SignChanges;
        }

        public OperationCounter MultiplyBy(int factor)
        {
            return new OperationCounter
            {
                Multiplications = Multiplications * factor,
                AdditionsSubtractions = AdditionsSubtractions * factor,
                SignChanges = SignChanges * factor
            };
        }

        public override string ToString()
        {
            return
                "Multiplications = " + Multiplications +
                ", Additions/Soustractions = " + AdditionsSubtractions +
                ", Changements de signe = " + SignChanges +
                ", Total = " + Total();
        }
    }
}