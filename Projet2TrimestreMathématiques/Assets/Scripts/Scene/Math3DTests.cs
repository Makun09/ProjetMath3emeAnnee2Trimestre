using UnityEngine;

public class Math3DTests : MonoBehaviour
{
    void Start()
    {
        TestVectors();
        TestQuaternions();
        TestMatrices();
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
}