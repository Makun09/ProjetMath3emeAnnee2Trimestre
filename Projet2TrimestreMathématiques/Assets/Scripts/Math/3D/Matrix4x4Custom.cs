using System;

public class Matrix4x4Custom
{
    public double M11, M12, M13, M14;
    public double M21, M22, M23, M24;
    public double M31, M32, M33, M34;
    public double M41, M42, M43, M44;

    public Matrix4x4Custom(
        double m11, double m12, double m13, double m14,
        double m21, double m22, double m23, double m24,
        double m31, double m32, double m33, double m34,
        double m41, double m42, double m43, double m44)
    {
        M11 = m11; M12 = m12; M13 = m13; M14 = m14;
        M21 = m21; M22 = m22; M23 = m23; M24 = m24;
        M31 = m31; M32 = m32; M33 = m33; M34 = m34;
        M41 = m41; M42 = m42; M43 = m43; M44 = m44;
    }

    public Matrix4x4Custom Multiply(Matrix4x4Custom other)
    {
        return new Matrix4x4Custom(
            M11 * other.M11 + M12 * other.M21 + M13 * other.M31 + M14 * other.M41,
            M11 * other.M12 + M12 * other.M22 + M13 * other.M32 + M14 * other.M42,
            M11 * other.M13 + M12 * other.M23 + M13 * other.M33 + M14 * other.M43,
            M11 * other.M14 + M12 * other.M24 + M13 * other.M34 + M14 * other.M44,

            M21 * other.M11 + M22 * other.M21 + M23 * other.M31 + M24 * other.M41,
            M21 * other.M12 + M22 * other.M22 + M23 * other.M32 + M24 * other.M42,
            M21 * other.M13 + M22 * other.M23 + M23 * other.M33 + M24 * other.M43,
            M21 * other.M14 + M22 * other.M24 + M23 * other.M34 + M24 * other.M44,

            M31 * other.M11 + M32 * other.M21 + M33 * other.M31 + M34 * other.M41,
            M31 * other.M12 + M32 * other.M22 + M33 * other.M32 + M34 * other.M42,
            M31 * other.M13 + M32 * other.M23 + M33 * other.M33 + M34 * other.M43,
            M31 * other.M14 + M32 * other.M24 + M33 * other.M34 + M34 * other.M44,

            M41 * other.M11 + M42 * other.M21 + M43 * other.M31 + M44 * other.M41,
            M41 * other.M12 + M42 * other.M22 + M43 * other.M32 + M44 * other.M42,
            M41 * other.M13 + M42 * other.M23 + M43 * other.M33 + M44 * other.M43,
            M41 * other.M14 + M42 * other.M24 + M43 * other.M34 + M44 * other.M44
        );
    }

    public Vector4Custom Multiply(Vector4Custom vector)
    {
        return new Vector4Custom(
            M11 * vector.X + M12 * vector.Y + M13 * vector.Z + M14 * vector.W,
            M21 * vector.X + M22 * vector.Y + M23 * vector.Z + M24 * vector.W,
            M31 * vector.X + M32 * vector.Y + M33 * vector.Z + M34 * vector.W,
            M41 * vector.X + M42 * vector.Y + M43 * vector.Z + M44 * vector.W
        );
    }
    
    public QuaternionCustom ToQuaternion()
    {
        if (!IsQuaternionMatrix())
            throw new Exception("Cette matrice ne correspond pas à une matrice de quaternion valide.");

        return new QuaternionCustom(M11, M21, M31, M41);
    }
    
    public bool IsQuaternionMatrix(double tolerance = 0.0001)
    {
        bool row1 = AreApproximatelyEqual(M12, -M21, tolerance) &&
                    AreApproximatelyEqual(M13, -M31, tolerance) &&
                    AreApproximatelyEqual(M14, -M41, tolerance);

        bool row2 = AreApproximatelyEqual(M22, M11, tolerance) &&
                    AreApproximatelyEqual(M23, -M41, tolerance) &&
                    AreApproximatelyEqual(M24, M31, tolerance);

        bool row3 = AreApproximatelyEqual(M32, M41, tolerance) &&
                    AreApproximatelyEqual(M33, M11, tolerance) &&
                    AreApproximatelyEqual(M34, -M21, tolerance);

        bool row4 = AreApproximatelyEqual(M42, -M31, tolerance) &&
                    AreApproximatelyEqual(M43, M21, tolerance) &&
                    AreApproximatelyEqual(M44, M11, tolerance);

        return row1 && row2 && row3 && row4;
    }
    
    public bool ApproximatelyEquals(Matrix4x4Custom other, double tolerance = 0.0001)
    {
        return AreApproximatelyEqual(M11, other.M11, tolerance) &&
               AreApproximatelyEqual(M12, other.M12, tolerance) &&
               AreApproximatelyEqual(M13, other.M13, tolerance) &&
               AreApproximatelyEqual(M14, other.M14, tolerance) &&

               AreApproximatelyEqual(M21, other.M21, tolerance) &&
               AreApproximatelyEqual(M22, other.M22, tolerance) &&
               AreApproximatelyEqual(M23, other.M23, tolerance) &&
               AreApproximatelyEqual(M24, other.M24, tolerance) &&

               AreApproximatelyEqual(M31, other.M31, tolerance) &&
               AreApproximatelyEqual(M32, other.M32, tolerance) &&
               AreApproximatelyEqual(M33, other.M33, tolerance) &&
               AreApproximatelyEqual(M34, other.M34, tolerance) &&

               AreApproximatelyEqual(M41, other.M41, tolerance) &&
               AreApproximatelyEqual(M42, other.M42, tolerance) &&
               AreApproximatelyEqual(M43, other.M43, tolerance) &&
               AreApproximatelyEqual(M44, other.M44, tolerance);
    }
    
    private bool AreApproximatelyEqual(double a, double b, double tolerance)
    {
        return System.Math.Abs(a - b) < tolerance;
    }

    public static Matrix4x4Custom Identity()
    {
        return new Matrix4x4Custom(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );
    }

    public override string ToString()
    {
        return $"[{M11}, {M12}, {M13}, {M14}]\n" +
               $"[{M21}, {M22}, {M23}, {M24}]\n" +
               $"[{M31}, {M32}, {M33}, {M34}]\n" +
               $"[{M41}, {M42}, {M43}, {M44}]";
    }
}