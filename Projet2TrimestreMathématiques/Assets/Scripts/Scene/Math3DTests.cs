using UnityEngine;

public class Math3DTests : MonoBehaviour
{
    void Start()
    {
        //TestVectors();
        //TestQuaternions();
        //TestMatrices();
        //TestQuaternionMatrixConversion();
        //TestCourseExampleRotationAroundOz();
        //TestCourseExampleRotationAxisIJK();
        TestAdditionalRotationExample();
    }

    void TestVectors()
    {
        Vector3Custom v1 = new Vector3Custom(1, 0, 0);
        Vector3Custom v2 = new Vector3Custom(0, 1, 0);

        Debug.Log("Produit scalaire v1 · v2 = " + v1.Dot(v2));
        Debug.Log("Produit vectoriel v1 x v2 = " + v1.Cross(v2));
    }

    void TestQuaternions()
    {
        QuaternionCustom q1 = new QuaternionCustom(1, 2, 3, 4);
        QuaternionCustom q2 = new QuaternionCustom(2, 1, 0, 3);

        Debug.Log("q1 + q2 = " + q1.Add(q2));
        Debug.Log("q1 * q2 = " + q1.Multiply(q2));
        Debug.Log("Conjugué de q1 = " + q1.Conjugate());
        Debug.Log("Norme de q1 = " + q1.Norm());
        Debug.Log("q1 normalisé = " + q1.Normalize());
    }

    void TestMatrices()
    {
        Matrix3x3 identity3 = Matrix3x3.Identity();
        Vector3Custom v = new Vector3Custom(3, 4, 5);

        Debug.Log("Matrice identité 3x3 appliquée à v = " + identity3.Multiply(v));

        Matrix4x4Custom identity4 = Matrix4x4Custom.Identity();
        Vector4Custom v4 = new Vector4Custom(1, 2, 3, 1);

        Debug.Log("Matrice identité 4x4 appliquée à v4 = " + identity4.Multiply(v4));
    }
    
    void TestQuaternionMatrixConversion()
    {
        QuaternionCustom q = new QuaternionCustom(1, 2, 3, 4);

        Matrix4x4Custom matrix = q.ToMatrix4x4();
        QuaternionCustom quaternionFromMatrix = matrix.ToQuaternion();

        Debug.Log("Quaternion original = " + q);
        Debug.Log("Matrice du quaternion = \n" + matrix);
        Debug.Log("Quaternion récupéré depuis la matrice = " + quaternionFromMatrix);

        QuaternionCustom q1 = new QuaternionCustom(1, 2, 3, 4);
        QuaternionCustom q2 = new QuaternionCustom(2, 1, 0, 3);

        QuaternionCustom quaternionProduct = q1.Multiply(q2);
        Matrix4x4Custom matrixFromQuaternionProduct = quaternionProduct.ToMatrix4x4();

        Matrix4x4Custom matrixProduct = q1.ToMatrix4x4().Multiply(q2.ToMatrix4x4());

        Debug.Log("Matrix(q1 * q2) = \n" + matrixFromQuaternionProduct);
        Debug.Log("Matrix(q1) * Matrix(q2) = \n" + matrixProduct);

        bool compatible = matrixFromQuaternionProduct.ApproximatelyEquals(matrixProduct);

        Debug.Log("Compatibilité Matrix(q1 * q2) = Matrix(q1) * Matrix(q2) : " + compatible);
    }
    
    void TestCourseExampleRotationAroundOz()
    {
        Vector3Custom axis = new Vector3Custom(0, 0, 1);
        double angle = 90;

        QuaternionCustom q = QuaternionCustom.FromAxisAngle(axis, angle);
        Matrix3x3 matrix = q.ToRotationMatrix3x3();

        Vector3Custom point = new Vector3Custom(1, 0, 0);

        Vector3Custom rotatedWithQuaternion = q.RotatePoint(point);
        Vector3Custom rotatedWithMatrix = matrix.Multiply(point);

        Debug.Log("=== Exemple cours 1 : rotation 90° autour de Oz ===");
        Debug.Log("Quaternion attendu ≈ 0.7071 + 0i + 0j + 0.7071k");
        Debug.Log("Quaternion obtenu = " + q);

        Debug.Log("Matrice attendue ≈ ");
        Debug.Log("[0, -1, 0]\n[1, 0, 0]\n[0, 0, 1]");
        Debug.Log("Matrice obtenue = \n" + matrix);

        Debug.Log("Point original = " + point);
        Debug.Log("Point attendu après rotation ≈ (0, 1, 0)");
        Debug.Log("Point obtenu avec quaternion = " + rotatedWithQuaternion);
        Debug.Log("Point obtenu avec matrice = " + rotatedWithMatrix);
    }
    
    void TestCourseExampleRotationAxisIJK()
    {
        Vector3Custom axis = new Vector3Custom(1, 1, 1);
        double angle = 120;

        QuaternionCustom q = QuaternionCustom.FromAxisAngle(axis, angle);
        Matrix3x3 matrix = q.ToRotationMatrix3x3();

        Vector3Custom point = new Vector3Custom(2, 3, 4);

        Vector3Custom rotatedWithQuaternion = q.RotatePoint(point);
        Vector3Custom rotatedWithMatrix = matrix.Multiply(point);

        Debug.Log("=== Exemple cours 2 : rotation axe i+j+k, angle 2π/3 ===");
        Debug.Log("Quaternion attendu = 0.5 + 0.5i + 0.5j + 0.5k");
        Debug.Log("Quaternion obtenu = " + q);

        Debug.Log("Vecteur original = " + point);
        Debug.Log("Vecteur attendu après rotation ≈ (4, 2, 3)");
        Debug.Log("Vecteur obtenu avec quaternion = " + rotatedWithQuaternion);
        Debug.Log("Vecteur obtenu avec matrice = " + rotatedWithMatrix);
    }
    
    void TestAdditionalRotationExample()
    {
        Debug.Log("=== Exemple proposé : rotation 180° autour de X ===");

        Vector3Custom axis = new Vector3Custom(1, 0, 0);
        double angle = 180;

        QuaternionCustom rotationQuaternion = QuaternionCustom.FromAxisAngle(axis, angle);
        Matrix3x3 rotationMatrix = rotationQuaternion.ToRotationMatrix3x3();

        Vector3Custom point = new Vector3Custom(0, 1, 0);

        Vector3Custom rotatedWithQuaternion = rotationQuaternion.RotatePoint(point);
        Vector3Custom rotatedWithMatrix = rotationMatrix.Multiply(point);

        Debug.Log("Axe = " + axis);
        Debug.Log("Angle = " + angle + "°");
        Debug.Log("Point original = " + point);
        Debug.Log("Résultat attendu ≈ (0, -1, 0)");

        Debug.Log("Quaternion utilisé = " + rotationQuaternion);
        Debug.Log("Matrice utilisée = \n" + rotationMatrix);

        Debug.Log("Résultat avec quaternion = " + rotatedWithQuaternion);
        Debug.Log("Résultat avec matrice = " + rotatedWithMatrix);
    }
    
}